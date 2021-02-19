using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.API.ActionFilters;
using ReadingIsGood.Business.Services.Interfaces;
using ReadingIsGood.Domain.Models;
using ReadingIsGood.Model.Requests;
using System.Threading.Tasks;

namespace ReadingIsGood.API.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPut("UpdateStock")]
        [ProducesResponseType(typeof(ServiceResult), 200)]
        [ProducesResponseType(typeof(ServiceResult), 400)]
        [ValidationFilter]
        public async Task<ActionResult<ServiceResult>> UpdateStock([FromBody] UpdateStockRequest request)
        {
            var result = await _bookService.UpdateStockAsync(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(ServiceResult.Error(result.Message));
        }
    }
}