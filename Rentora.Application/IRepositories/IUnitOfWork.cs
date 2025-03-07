using System;

namespace Rentora.Application.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository products { get; }
        Task Save();
    }
}
