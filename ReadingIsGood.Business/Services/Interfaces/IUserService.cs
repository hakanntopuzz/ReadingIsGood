using ReadingIsGood.Domain.Requests;
using ReadingIsGood.Model.Entities;
using ReadingIsGood.Model.Models;
using System.Threading.Tasks;

namespace ReadingIsGood.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<GenericServiceResult<User>> AuthenticateAsync(AuthenticateUserRequest request);
    }
}
