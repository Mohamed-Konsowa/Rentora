using System;

namespace Rentora.Application.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository products { get; }
        IUserRepository users { get; }
        IEmailRepository emails { get; }
        Task Save();
    }
}
