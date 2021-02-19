using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.API.ActionFilters;
using ReadingIsGood.Business.Services.Interfaces;
using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.Requests;
using ReadingIsGood.Model.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet("GetOrder")]
        [ProducesResponseType(typeof(OrderDto), 200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<OrderDto>> GetOrder(int orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId);

            if(order == null)
            {
                return NoContent();
            }

            return Ok(order);
        }

        [HttpGet("GetCustomerOrders")]
        [ProducesResponseType(typeof(List<OrderDto>), 200)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<List<OrderDto>>> GetCustomerOrders(int customerId)
        {
            var customerOrders = await _orderService.GetCustomerOrderAsync(customerId);

            if(customerOrders == null ||!customerOrders.Any())
            {
                return NoContent();
            }

            return Ok(customerOrders);
        }

        [HttpPost("CreateOrder")]
        [ProducesResponseType(typeof(Int32ServiceResult), 200)]
        [ProducesResponseType(typeof(ServiceResult), 400)]
        [ValidationFilter]
        public async Task<ActionResult<Int32ServiceResult>> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var result = await _orderService.AddOrderAsync(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(ServiceResult.Error(result.Message));
        }
    }
}
