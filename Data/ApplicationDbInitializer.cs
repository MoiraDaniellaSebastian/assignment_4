using assignment_4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace assignment_4.Data 
{
    public static class ApplicationDbInitializer
    {
        public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um)
        {
            db.Database.EnsureDeleted(); 
            db.Database.EnsureCreated();  
            db.SaveChanges();
        }
     }
   }     