using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BookStoreManagement.Domain.Context;

namespace BookStoreManagement.Service.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Properties

        public BookStoreDBContext DbContext { get; set; }

    

        #endregion Properties

        #region Constructors

        public Repository(BookStoreDBContext dbContext)
        {
            this.DbContext = dbContext;
           
        }

        #endregion Constructors

        #region Methods

        public virtual T Add(T entity)
        {
            return this.DbContext.Set<T>().Add(entity).Entity;
        }

        public virtual T Remove(T entity)
        {
            return this.DbContext.Set<T>().Remove(entity).Entity;
        }

        public virtual void RemoveRange(List<T> entities)
        {
            this.DbContext.RemoveRange(entities);
        }

        public virtual void AddRange(List<T> entities)
        {
            this.DbContext.AddRange(entities);
        }

        public virtual IQueryable<T> GetAll()
        {
            return this.DbContext.Set<T>().AsQueryable();
        }

       

        public virtual void SaveChanges(string username)
        {
            DateTime now = DateTime.UtcNow;

            foreach (var entityEntry in this.DbContext.ChangeTracker.Entries<T>())
            {
                if (entityEntry.State == EntityState.Modified)
                {
                    // Assuming the entity has 'UpdatedAt' property
                    var updatedAtProp = entityEntry.Entity.GetType().GetProperty("UpdatedAt");
                    if (updatedAtProp != null)
                    {
                        updatedAtProp.SetValue(entityEntry.Entity, now);
                    }
                }
                else if (entityEntry.State == EntityState.Added)
                {
                    // Assuming the entity has 'CreatedAt' property
                    var createdAtProp = entityEntry.Entity.GetType().GetProperty("CreatedAt");
                    if (createdAtProp != null)
                    {
                        createdAtProp.SetValue(entityEntry.Entity, now);
                    }
                }
            }

            this.DbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return this.DbContext.SaveChangesAsync();
        }

        public virtual void Attach(T entity)
        {
            this.DbContext.Attach(entity);
        }

        // Assuming that the entity has a 'DeletedAt' property for soft deletion
        public virtual void SoftDelete(T entity)
        {
            var deletedAtProp = entity.GetType().GetProperty("DeletedAt");
            if (deletedAtProp != null)
            {
                deletedAtProp.SetValue(entity, DateTime.UtcNow);
            }
        }

        public void SoftDeleteRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                this.SoftDelete(entity);
            }
        }

        public virtual bool IsPropValueUsed(string propertyName, string value)
        {
            var query = this.GetAll().Where(e => EF.Property<string>(e, propertyName) == value);
            return query.Any();
        }

        public virtual bool IsPropValueUsed(string propName, string propValue, Type? propType = null)
        {
            bool isUsed = this.IsPropValueUsed(propName, propValue);
            if (isUsed)
            {
                throw new Exception("The selected item is currently in use and cannot be deleted.");
            }
            return isUsed;
        }

        #endregion Methods
    }
}
