using Microsoft.EntityFrameworkCore.Storage;
using ReadingIsGood.Data.Repositories;
using ReadingIsGood.Model.Entities;
using System;
using System.Threading.Tasks;

namespace ReadingIsGood.Business
{
    public interface IUnitofWork : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();

        IRepository<Customer> CustomerRepository { get; }
        IRepository<Order> OrderRepository { get; }

        IRepository<Book> BookRepository { get; }

        IRepository<User> UserRepository { get; }
    }
}
