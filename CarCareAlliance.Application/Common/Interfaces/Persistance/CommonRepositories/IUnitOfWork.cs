﻿using CarCareAlliance.Domain.Common.Models;

namespace CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IGenericRepository<TEntity, TId> GetRepository<TEntity, TId>(
            bool hasCustomRepository = false)
                where TEntity : Entity<TId>
                where TId : ValueObject;
    }
}
