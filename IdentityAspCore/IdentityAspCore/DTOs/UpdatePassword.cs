using System.ComponentModel.DataAnnotations;

namespace IdentityAspCore.DTOs
{
    public class UpdatePassword
    {

        public string oldPassword { get; set; } = string.Empty;


        public string newPassword { get; set; } = string.Empty;




    }


}
