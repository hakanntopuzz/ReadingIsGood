using AutoMapper;
using Microsoft.Extensions.Logging;
using ReadingIsGood.Business.Services.Interfaces;
using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.Requests;
using ReadingIsGood.Model.Dtos;
using ReadingIsGood.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Business.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly ILogger<OrderService> _logger;

        public OrderService(ILogger<OrderService> logger,
            IUnitofWork unitofWork,
            IMapper mapper)
            : base(mapper, unitofWork)
        {
            _logger = logger;
        }

        public async Task<OrderDto> GetOrderAsync(int orderId)
        {
            try
            {
                var entity = await _unitofWork.OrderRepository.FirstOrDefaultAsync(q => q.Id == orderId);
                var order = _mapper.Map<OrderDto>(entity);

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public ICollection<Order> GetOrders(int? customerId, int? bookId)
        {
            try
            {
                var query = _unitofWork.OrderRepository.Table.AsQueryable();

                if (bookId.HasValue)
                {
                    query = query.Where(q => q.BookId == bookId.Value);
                }

                if (customerId.HasValue)
                {
                    query = query.Where(q => q.CustomerId == customerId.Value);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<ICollection<OrderDto>> GetCustomerOrderAsync(int customerId)
        {
            try
            {
                var customerOrders = await _unitofWork.OrderRepository.ToListAsync(q => q.CustomerId == customerId);
                var orderList = _mapper.Map<List<Order>, List<OrderDto>>(customerOrders);

                return orderList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<Int32ServiceResult> AddOrderAsync(CreateOrderRequest request)
        {
            try
            {
                using (var transaction = _unitofWork.BeginTransaction())
                {
                    var book = _unitofWork.BookRepository.Table.Find(request.BookId);

                    if (book.Stock == 0)
                    {
                        return Int32ServiceResult.Error("Book is out of stock.");
                    }

                    book.Stock = book.Stock - 1;
                    _unitofWork.SaveChanges();

                    var order = _mapper.Map<Order>(request);
                    order.CreateDate = DateTime.Now;

                    _unitofWork.OrderRepository.Add(order);
                    
                    await _unitofWork.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Int32ServiceResult.Success(order.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Int32ServiceResult.Error("An error occurred while create order.");
            }
        }
    }
}