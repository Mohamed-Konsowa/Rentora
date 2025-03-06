using System;

namespace RentoraAPI.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository products { get; }
        Task Save();
    }
}
