namespace Demo.HandleResponses
{
    public class ApiValidationErrorResponse : ApiException
    {
        public ApiValidationErrorResponse() : base(400) //400 refer to the first parameter which is the statusCode 
        {
        }

        public IEnumerable<string> Errors { get; set; }


    }
}
