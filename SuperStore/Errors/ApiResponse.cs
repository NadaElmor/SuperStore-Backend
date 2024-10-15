
namespace SuperStore.Errors
{
    public class ApiResponse
    {
      public  int StatusCode { get; set; }
     public string? ErrorMessage { get; set; } = string.Empty;

        public ApiResponse(int statusCode, string? errorMessage=null) {
            
            StatusCode = statusCode;
            ErrorMessage = errorMessage?? GetErrorMessageByStatusCode(statusCode);
        
        }

        private string? GetErrorMessageByStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request , you made",
                401 => "You are unauthorized",
                404 => "Resources not found",
                500 => "Server Error",
                _=> null
            };
        }
    }
}
