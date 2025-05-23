using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ECommerceApi.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public ApiResponse(int statusCode , string errorMessage = null) 
        {
        StatusCode = statusCode ;
        ErrorMessage = errorMessage ?? GetDefaultMessage(StatusCode);
        }

        private string? GetDefaultMessage(int? statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "You are not Authorized",
                404 => "Resource Not Found",
                500 => "Internal Server Error",
                _ => null
            };
        }
    }
}
