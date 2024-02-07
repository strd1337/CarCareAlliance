using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Common.Services;
using CarCareAlliance.Domain.Common.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CarCareAlliance.Infrastructure.Persistance.Repositories.Common
{
    public class UnitOfWork(
        CarCareAllianceDbContext dbContext,
        IDateTimeProvider dateTimeProvider) : IUnitOfWork
    {
        private readonly CarCareAllianceDbContext dbContext = dbContext;
        private bool isDisposed;
        private readonly Dictionary<Type, object> repositories = [];
        private readonly IDateTimeProvider dateTimeProvider = dateTimeProvider;

        public IGenericRepository<TEntity, TId> GetRepository<TEntity, TId>(
            bool hasCustomRepository = false)
                where TEntity : Entity<TId>
                where TId : ValueObject
        {
            if (hasCustomRepository)
            {
                var customRepository = dbContext
                    .GetService<IGenericRepository<TEntity, TId>>();

                if (customRepository is not null)
                {
                    return customRepository;
                }
            }

            var type = typeof(TEntity);
            if (!repositories.TryGetValue(type, out object? value))
            {
                value = new GenericRepository<TEntity, TId>(dbContext);
                repositories[type] = value;
            }

            return (IGenericRepository<TEntity, TId>)value;
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    repositories?.Clear();
                    dbContext.Dispose();
                }
            }
            isDisposed = true;
        }
    }
}