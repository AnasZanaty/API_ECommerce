namespace Demo.HandleResponses
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode , string message = null) 
        { 
            StatusCode = statusCode;
            Message = message ?? GetDeffaultMessageForStatusCode(statusCode);
        
        }
        public int? StatusCode { get; set; }
        public string? Message { get; set; }
        private string GetDeffaultMessageForStatusCode (int code)
        
            //match pattern
            => code switch
            {
                400 => "Bad Request",
                401 => "You are not authorized!",
                404=> "Resourse not found!",
                500 => "Internal Server Error",
                  _ => null
            };
        
        
    }
}
