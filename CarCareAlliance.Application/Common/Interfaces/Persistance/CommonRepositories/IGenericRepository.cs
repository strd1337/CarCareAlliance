﻿using CarCareAlliance.Domain.Common.Models;
using System.Linq.Expressions;

namespace CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories
{
    public interface IGenericRepository<TEntity, TId>
        where TEntity : Entity<TId>
        where TId : ValueObject
    {
        Task<TEntity?> GetByIdAsync(
            TId id,
            CancellationToken cancellationToken = default);

        Task<TEntity?> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);
         
        Task<TEntity?> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default,
            params string[] includes);

        Task AddAsync(TEntity entity,
            CancellationToken cancellationToken = default);
        
        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(List<TEntity> entities);

        Task RemoveAsync(TEntity entity);

        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(string include);
        IQueryable<TEntity> GetAll(string include, string include2);
        IQueryable<TEntity> GetAll(params string[] includes);

        Task<ICollection<TEntity>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<ICollection<TEntity>> GetWhereAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        IQueryable<TEntity> GetWhere(
            Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetWhere(
            Expression<Func<TEntity, bool>> predicate,
            string include);

        Task<int> CountAllAsync(
            CancellationToken cancellationToken = default);

        Task<int> CountWhereAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);
    }
}
