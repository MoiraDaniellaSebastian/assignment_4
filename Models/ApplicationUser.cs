using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace assignment_4.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [MinLength(3)]
    [MaxLength(25)]
    [DataType(DataType.Text)]
    public string Nickname { get; set; } = string.Empty;
    
}
