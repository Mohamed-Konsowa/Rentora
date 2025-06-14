﻿using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Rentora.Application.IRepositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IProductRepository products { get; }
        IUserRepository users { get; }
        IEmailRepository emails { get; }
        ICartRepository carts { get; }
        IFavoriteRepository favorites { get; }
        IRentRepository rentals { get; }
        IReviewRepository reviews { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task SaveChangesAsync();
    }
}
