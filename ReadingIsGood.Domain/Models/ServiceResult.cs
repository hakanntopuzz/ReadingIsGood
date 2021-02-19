namespace ReadingIsGood.Domain.Models
{
    public class ServiceResult : BaseServiceResult
    {
        #region ctor

        ServiceResult(bool isSuccess, string message = null)
            : base(isSuccess, message)
        {
        }

        #endregion

        public static ServiceResult Success()
        {
            return new ServiceResult(true);
        }

        public static ServiceResult Error(string message)
        {
            return new ServiceResult(false, message);
        }
    }
}