using AutoMapper;
using Microsoft.Extensions.Logging;
using ReadingIsGood.Business.Services.Interfaces;
using ReadingIsGood.Domain.Requests;
using ReadingIsGood.Model.Entities;
using ReadingIsGood.Model.Models;
using System;
using System.Threading.Tasks;

namespace ReadingIsGood.Business.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger,
             IUnitofWork unitofWork,
             IMapper mapper)
             : base(mapper, unitofWork)
        {
            _logger = logger;
        }

        public async Task<GenericServiceResult<User>> AuthenticateAsync(AuthenticateUserRequest request)
        {
            try
            {
                var entity = await _unitofWork.UserRepository.SingleOrDefaultAsync(q => q.Username.Equals(request.Username) && q.Password.Equals(request.Password));

                if (entity == null)
                {
                    return GenericServiceResult<User>.Error("Invalid username or password.");
                }

                return GenericServiceResult<User>.Success(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return GenericServiceResult<User>.Error("An error occurred while authenticate user.");
            }
        }
    }
}