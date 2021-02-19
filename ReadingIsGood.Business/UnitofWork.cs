using Microsoft.EntityFrameworkCore.Storage;
using ReadingIsGood.Data;
using ReadingIsGood.Data.Repositories;
using ReadingIsGood.Model.Entities;
using System;
using System.Threading.Tasks;

namespace ReadingIsGood.Business
{
    public class UnitofWork : IUnitofWork
    {
        private bool _disposed;
        private readonly ReadingDBContext _dbContext;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<User> _userRepository;

        public UnitofWork(ReadingDBContext dbContext,
            IRepository<Customer> customerRepository,
            IRepository<Order> orderRepository,
            IRepository<Book> bookRepository,
            IRepository<User> userRepository)
        {
            _dbContext = dbContext;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }

        public IRepository<Customer> CustomerRepository => _customerRepository;

        public IRepository<Order> OrderRepository => _orderRepository;

        public IRepository<Book> BookRepository => _bookRepository;

        public IRepository<User> UserRepository => _userRepository;

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}