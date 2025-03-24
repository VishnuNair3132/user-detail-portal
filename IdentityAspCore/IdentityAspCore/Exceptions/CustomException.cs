namespace IdentityAspCore.Exceptions
{
    public class CustomException:Exception
    {
        public string ExceptionType { get; }

        public CustomException(string Message,string ExceptionType):base(Message)
        { 
            this.ExceptionType = ExceptionType;
        }


    }
}
