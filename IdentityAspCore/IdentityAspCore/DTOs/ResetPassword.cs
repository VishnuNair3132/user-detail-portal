using System.ComponentModel.DataAnnotations;

namespace IdentityAspCore.DTOs
{
    public class ResetPassword
    {



        public string NewPassword { get; set; } = string.Empty;



        public string Email { get; set; } = string.Empty;


        public string Token { get; set; } = string.Empty;
    }
}
