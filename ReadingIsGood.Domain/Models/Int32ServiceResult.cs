
namespace ReadingIsGood.Domain.Models
{
    public class Int32ServiceResult : BaseServiceResult
    {
        #region ctor

        public int Value { get; set; }

        Int32ServiceResult(int value, bool isSuccess, string message = null)
          : base(isSuccess, message)
        {
            Value = value;
        }

        #endregion

        public static Int32ServiceResult Success(int value)
        {
            return new Int32ServiceResult(value, true);
        }

        public static Int32ServiceResult Error(string message)
        {
            return new Int32ServiceResult(0, false, message);
        }
    }
}
