using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.API.ActionFilters;
using ReadingIsGood.Business.Services.Interfaces;
using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.Requests;
using System.Threading.Tasks;

namespace ReadingIsGood.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("CreateCustomer")]
        [ProducesResponseType(typeof(Int32ServiceResult), 200)]
        [ProducesResponseType(typeof(ServiceResult), 400)]
        [ValidationFilter]
        public async Task<ActionResult<Int32ServiceResult>> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            var result = await _customerService.AddCustomerAsync(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(ServiceResult.Error(result.Message));
        }
    }
}