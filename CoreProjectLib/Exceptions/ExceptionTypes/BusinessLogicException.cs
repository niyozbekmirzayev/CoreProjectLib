
namespace Core.Exceptions.ExceptionTypes
{
    public class BusinessLogicException : Exception
    {
        public int StatusCode { get; }
        public BusinessLogicException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
