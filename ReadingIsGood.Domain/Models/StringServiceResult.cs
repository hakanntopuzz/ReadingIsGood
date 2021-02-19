
namespace ReadingIsGood.Domain.Models
{
    public class StringServiceResult : BaseServiceResult
    {
        #region ctor

        public string Value { get; set; }

        StringServiceResult(string value, bool isSuccess, string message = null)
          : base(isSuccess, message)
        {
            Value = value;
        }

        #endregion

        public static StringServiceResult Success(string value)
        {
            return new StringServiceResult(value, true);
        }

        public static StringServiceResult Error(string message)
        {
            return new StringServiceResult(string.Empty, false, message);
        }
    }
}
