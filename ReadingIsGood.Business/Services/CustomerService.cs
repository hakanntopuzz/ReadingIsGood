using AutoMapper;
using Microsoft.Extensions.Logging;
using ReadingIsGood.Business.Services.Interfaces;
using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.Requests;
using ReadingIsGood.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Business.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger,
            IUnitofWork unitofWork,
            IMapper mapper)
            :base(mapper, unitofWork)
        {
            _logger = logger;
        }

        public async Task<Int32ServiceResult> AddCustomerAsync(CreateCustomerRequest request)
        {
            try
            {
                var customer = _mapper.Map<Customer>(request);
                await _unitofWork.CustomerRepository.CreateAsync(customer);

                return Int32ServiceResult.Success(customer.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Int32ServiceResult.Error("An error occurred while create customer.");
            }
        }

        public ICollection<Customer> GetCustomers(int? id, string? name)
        {
            try
            {
                var query = _unitofWork.CustomerRepository.Table.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(q => q.Id == id.Value);
                }

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(q => q.Name.Contains(name));
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}