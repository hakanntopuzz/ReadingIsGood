using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.Requests;
using ReadingIsGood.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingIsGood.Business.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Int32ServiceResult> AddCustomerAsync(CreateCustomerRequest request);

        ICollection<Customer> GetCustomers(int? id, string? name);
    }
}
