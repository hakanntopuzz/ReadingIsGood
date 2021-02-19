namespace ReadingIsGood.API
{
    public interface ITokenGenerator
    {
        string GenerateToken(int userId);
    }
}