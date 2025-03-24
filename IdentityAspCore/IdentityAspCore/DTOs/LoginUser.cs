namespace IdentityAspCore.DTOs
{
    public class LoginUser
    {


        public string  Username { get; set; }
        public string Password { get; set; }


        public LoginUser(string Username,string Password)
        {
            this.Username = Username;
            this.Password = Password;
            
        }


    }
}
