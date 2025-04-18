﻿using CoreProjectLib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CoreProjectLib.Data.Repositories
{
    public abstract class BaseRepository<TEntity, TDbContext> : IBaseRepository<TEntity, TDbContext>
            where TEntity : BaseEntity
            where TDbContext : BaseDbContext
    {
        private bool _disposed = false;

        public BaseRepository(TDbContext dbContext)
        {
            _disposed = false;
            DbContext = dbContext;
        }

        public DbSet<TEntity> Entities
        {
            get { return DbContext.Set<TEntity>(); }
        }

        public LocalView<TEntity> Local
        {
            get { return Entities.Local; }
        }

        public TDbContext DbContext { get; }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("Null value can not be added");
            }

            var entry = await DbContext.AddAsync(entity);

            return entry.Entity;

        }

        public virtual TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("Null value can not be added");
            }

            var entry = DbContext.Add(entity);

            return entry.Entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new Exception("Null value can not be added");
            }

            await DbContext.AddRangeAsync(entities);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Entities;
        }

        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            return await Entities.FirstOrDefaultAsync(l => l.Id == id);
        }

        public virtual void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("Null entity can not be removed");
            }

            if (entity is PersistentEntity)
            {
                (entity as PersistentEntity).State = Enums.EntityState.Deleted;
                DbContext.Update(entity);
            }
            else
            {
                DbContext.Remove(entity);
            }
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new Exception("Null entities can not be removed");
            }

            foreach (TEntity entity in entities)
            {
                if (entity is PersistentEntity)
                {
                    (entity as PersistentEntity).State = Enums.EntityState.Inactive;
                    DbContext.Update(entity);
                }
                else
                {
                    DbContext.Remove(entity);
                }
            }
        }

        public virtual TEntity Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new Exception("Null entity can not be updated");
            }

            entity.LastModifiedDate = DateTime.UtcNow;

            DbContext.Update(entity);

            return entity;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                throw new Exception("Null entities can not be updated");
            }

            foreach (var entity in entities)
            {
                entity.LastModifiedDate = DateTime.UtcNow;

                DbContext.Update(entity);
            }
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

        public virtual int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public virtual void Dispose()
        {
            if (DbContext != null && !_disposed)
            {
                DbContext.Dispose();
            }
            _disposed = true;
        }
    }
}
