using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BookStoreManagement.Service.Repository
{
    public interface IRepository<T> where T : class
    {
        // Add an entity without saving changes
        T Add(T entity);

        // Remove an entity without saving changes
        T Remove(T entity);

        // Remove multiple entities without saving changes
        void RemoveRange(List<T> entities);

        // Add multiple entities without saving changes
        void AddRange(List<T> entities);

        // Get queryable collection of entities
        IQueryable<T> GetAll();

        // Asynchronously save tracked entities changes
        Task<int> SaveChangesAsync();

        // Attach an entity to the context
        void Attach(T entity);

        // Soft delete an entity (if using soft deletion)
        void SoftDelete(T entity);

        // Soft delete multiple entities (if using soft deletion)
        void SoftDeleteRange(IEnumerable<T> entities);

        // Check if an entity with a specific property value exists
        bool IsPropValueUsed(string propertyName, string value);

        // Check if a property value is used and throw a business exception if provided
        bool IsPropValueUsed(string propName, string propValue, System.Type? propType = null);
    }
}
