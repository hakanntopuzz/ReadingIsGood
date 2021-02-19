
namespace ReadingIsGood.Model.Models
{
    public class GenericServiceResult<T> where T :class
    {
        public T Data { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public static GenericServiceResult<T> Success(T data)
        {
            return new GenericServiceResult<T>()
            {
                Data = data,
                IsSuccess = true
            };
        }

        public static GenericServiceResult<T> Error(string message)
        {
            return new GenericServiceResult<T>()
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}