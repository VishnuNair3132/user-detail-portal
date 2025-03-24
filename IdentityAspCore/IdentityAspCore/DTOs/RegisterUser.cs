namespace IdentityAspCore.DTOs
{
    public class RegisterUser
    {
        public string Firstname { get; set; } = String.Empty;

        public string Lastname { get; set; } = String.Empty;


        public string Email { set; get; } = String.Empty;



        public string Username { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;


        public string? Role { set; get; }

    }
}
