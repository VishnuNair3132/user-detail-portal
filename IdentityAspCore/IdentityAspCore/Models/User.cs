using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityAspCore.Models
{
    public class ApplicationUser:IdentityUser
    {


        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Email is required")]
        public string Firstname { get; set; } = string.Empty;



        [Required(ErrorMessage = "Lastname is required")]

        public string Lastname { get; set; } = string.Empty;


        public string? Role { get; set; }

    }
}
