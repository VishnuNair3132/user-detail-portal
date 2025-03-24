namespace IdentityAspCore.DTOs
{
    public  class  ApiResponse<T>
    {

        public bool status { get; set; } 

        public string message { set; get; } = string.Empty;


        public T? Body { set; get; }

        public ApiResponse(bool status,string message,T Body)
        { 
            this.status = status;
            this.message = message;
            this.Body = Body;
        }


        public ApiResponse(bool status,string message)
        {

            this.status = status;
            this.message = message;
        }

    }
}
