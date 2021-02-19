
namespace ReadingIsGood.Domain.Models
{
    public abstract class BaseServiceResult
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        protected BaseServiceResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
