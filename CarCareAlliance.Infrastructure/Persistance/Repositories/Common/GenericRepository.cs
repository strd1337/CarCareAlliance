﻿using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarCareAlliance.Infrastructure.Persistance.Repositories.Common
{
    public class GenericRepository<TEntity, TId>(
        CarCareAllianceDbContext dbContext) : IGenericRepository<TEntity, TId> 
            where TEntity : Entity<TId>
            where TId : ValueObject
    {
        protected CarCareAllianceDbContext dbContext = dbContext;

        public async Task<TEntity?> GetByIdAsync(
            TId id,
            CancellationToken cancellationToken = default)
        {
            return await dbContext
                .Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id,
                    cancellationToken);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await dbContext
                .Set<TEntity>()
                .FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task AddAsync(
            TEntity entity,
            CancellationToken cancellationToken = default)
        {
            await dbContext
                .Set<TEntity>()
                .AddAsync(entity, cancellationToken);
        }

        public Task UpdateAsync(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
            return Task.CompletedTask;
        }
        
        public Task UpdateAsync(List<TEntity> entities)
        {
            var updateTasks = new List<Task>();

            foreach (var entity in entities)
            {
                updateTasks.Add(UpdateAsync(entity));
            }

            return Task.WhenAll(updateTasks);
        }

        public Task RemoveAsync(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
            return Task.CompletedTask;
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>();
        }

        public async Task<ICollection<TEntity>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await dbContext
                .Set<TEntity>()
                .ToListAsync(cancellationToken);
        }

        public IQueryable<TEntity> GetAll(string include)
        {
            return dbContext
                .Set<TEntity>()
                .Include(include);
        }

        public IQueryable<TEntity> GetWhere(
            Expression<Func<TEntity, bool>> predicate)
        {
            return dbContext
                .Set<TEntity>()
                .Where(predicate);
        }

        public IQueryable<TEntity> GetWhere(
            Expression<Func<TEntity, bool>> predicate,
            string include)
        {
            return GetWhere(predicate).Include(include);
        }

        public IQueryable<TEntity> GetAll(string include, string include2)
        {
            return dbContext
                .Set<TEntity>()
                .Include(include)
                .Include(include2);
        }

        public async Task<int> CountAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await dbContext
                .Set<TEntity>()
                .CountAsync(cancellationToken);
        }

        public async Task<int> CountWhereAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await dbContext
                .Set<TEntity>()
                .CountAsync(predicate, cancellationToken);
        }

        public async Task<bool> ExistsAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return await dbContext
                .Set<TEntity>()
                .AnyAsync(predicate, cancellationToken);
        }

        public async Task<ICollection<TEntity>> GetWhereAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken)
        {
            return await dbContext
                .Set<TEntity>()
                .Where(predicate)
                .ToArrayAsync(cancellationToken);
        }

        public IQueryable<TEntity> GetAll(params string[] includes)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public async Task<TEntity?> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate, 
            CancellationToken cancellationToken = default, 
            params string[] includes)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query
                .FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
