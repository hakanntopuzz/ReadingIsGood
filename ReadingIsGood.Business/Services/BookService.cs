using AutoMapper;
using Microsoft.Extensions.Logging;
using ReadingIsGood.Business.Services.Interfaces;
using ReadingIsGood.Domain.Models;
using ReadingIsGood.Model.Requests;
using System;
using System.Threading.Tasks;

namespace ReadingIsGood.Business.Services
{
    public class BookService : BaseService, IBookService
    {
        private readonly ILogger<BookService> _logger;

        public BookService(ILogger<BookService> logger,
            IUnitofWork unitofWork,
            IMapper mapper)
            : base(mapper, unitofWork)
        {
            _logger = logger;
        }

        public async Task<ServiceResult> UpdateStockAsync(UpdateStockRequest request)
        {
            try
            {
                var entity =  await _unitofWork.BookRepository.FirstOrDefaultAsync(q => q.Id == request.BookId);

                if(entity == null)
                {
                    return ServiceResult.Error("Book is not found.");
                }

                entity.Stock += request.Stock;
                await _unitofWork.SaveChangesAsync();

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ServiceResult.Error("An error occurred while update stock.");
            }
        }
    }
}