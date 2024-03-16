using System.Net;

namespace CarCareAlliance.Presentation.Client.Common.Exceptions
{
    public class CustomHttpRequestException : Exception
    {
        public List<string> ErrorMessages { get; } = [];
        public HttpStatusCode StatusCode { get; }

        public CustomHttpRequestException(List<string> messages, HttpStatusCode statusCode) : base()
        {
            ErrorMessages = messages;
            StatusCode = statusCode;
        }

        public CustomHttpRequestException(
            string message, 
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
        {
            ErrorMessages = [message];
            StatusCode = statusCode;
        }
    }
}
