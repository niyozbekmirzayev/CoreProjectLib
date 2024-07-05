
namespace CoreProjectLib.Responses
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string ErrorMessage { get; set; }
    }
}
