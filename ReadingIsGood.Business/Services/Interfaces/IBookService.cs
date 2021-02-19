using ReadingIsGood.Domain.Models;
using ReadingIsGood.Model.Requests;
using System.Threading.Tasks;

namespace ReadingIsGood.Business.Services.Interfaces
{
    public interface IBookService
    {
        Task<ServiceResult> UpdateStockAsync(UpdateStockRequest request);
    }
}