using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.API;
using ReadingIsGood.API.ActionFilters;
using ReadingIsGood.Business.Services.Interfaces;
using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.Requests;
using System.Threading.Tasks;

namespace ReadingIsGood.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenGenerator _tokenGenerator;

        public UserController(IUserService userService,
            ITokenGenerator tokenGenerator)
        {
            _userService = userService;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("Authenticate")]
        [ProducesResponseType(typeof(StringServiceResult), 200)]
        [ProducesResponseType(typeof(ServiceResult), 400)]
        [ValidationFilter]
        public async Task<ActionResult<StringServiceResult>> Authenticate([FromBody] AuthenticateUserRequest request)
        {
            var result = await _userService.AuthenticateAsync(request);

            if (result.IsSuccess)
            {
                var jwtToken = _tokenGenerator.GenerateToken(result.Data.Id);

                return Ok(StringServiceResult.Success(jwtToken));
            }

            return BadRequest(ServiceResult.Error(result.Message));
        }
    }
}
