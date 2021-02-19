using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.Requests;
using ReadingIsGood.Model.Dtos;
using ReadingIsGood.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingIsGood.Business.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> GetOrderAsync(int orderId);
        Task<Int32ServiceResult> AddOrderAsync(CreateOrderRequest request);
        Task<ICollection<OrderDto>> GetCustomerOrderAsync(int customerId);
        ICollection<Order> GetOrders(int? customerId, int? bookId);
    }
}