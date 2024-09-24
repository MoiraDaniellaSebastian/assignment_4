using Microsoft.AspNetCore.Mvc;
using assignment_4.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using assignment_4.Models;
using Microsoft.EntityFrameworkCore;

public class BlogController : Controller {
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _um;

    public BlogController(ApplicationDbContext db, UserManager<ApplicationUser> um)
    {
        _db = db;
        _um = um;
    }

    // GET: My Blog Home Page
    public IActionResult Index()
    {
        var posts = _db.BlogPosts
            .Include(b => b.User)
            .OrderByDescending(b => b.TimeStamp)
            .ToList();

        return View(posts);
    }

    // GET: Add a new post to My Blog
    [Authorize]
    public IActionResult Add()
    {
        return View();
    }
    
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(BlogPost post)
    {
        var userId = _um.GetUserId(User);
    
        if (userId == null)
        {
            return BadRequest("User not found. Cannot add post.");
        }
    
        post.UserId = userId;
        post.TimeStamp = DateTime.Now;
    
        ModelState.Remove("UserId"); // Remove the ModelState error for UserId
    
        if (!ModelState.IsValid)
        {
            // Return the view with validation errors
            return View(post);
        }
    
        _db.Add(post);
        await _db.SaveChangesAsync();
    
        return RedirectToAction(nameof(Index));
    }

    // GET: Edit a post in My Blog
    [Authorize]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null){
            return BadRequest("Invalid post ID for My Blog.");
        }

        var post = await _db.BlogPosts.FindAsync(id);

        if (post == null){
            return NotFound("Post not found in My Blog.");
        }

        if (post.UserId != _um.GetUserId(User)){
            return Forbid("You are not authorized to edit this post in My Blog.");
        }

        ViewBag.Title = "Edit Post - My Blog";
        return View(post);
    }

    // POST: Edit a post in My Blog
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit(BlogPost post)
    {
        var userId = _um.GetUserId(User);

        if (!ModelState.IsValid){
            ViewBag.Title = "Edit Post - My Blog";
            return View(post);
        }

        var existingPost = await _db.BlogPosts.FindAsync(post.Id);

        if (existingPost == null){
            return NotFound("Post not found in My Blog.");
        }

        if (existingPost.UserId != userId){
            return Forbid("You are not authorized to edit this post in My Blog.");
        }

        existingPost.Title = post.Title;
        existingPost.Content = post.Content;
        existingPost.Summary = post.Summary;

        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index), new { message = "Post updated successfully in My Blog!" });
    }
}
