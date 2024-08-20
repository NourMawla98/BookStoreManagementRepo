namespace BookStoreManagement.Service.Repository
{
    public interface IBookStoreRepository
    {
        // Add an entity without saving changes
        T Add<T>(T entity) where T : class;

        // Remove an entity without saving changes
        T Remove<T>(T entity) where T : class;

        // Remove multiple entities without saving changes
        void RemoveRange<T>(List<T> entities) where T : class;

        // Add multiple entities without saving changes
        void AddRange<T>(List<T> entities) where T : class;

        // Get queryable collection of entities
        IQueryable<T> GetAll<T>() where T : class;

        // Asynchronously save tracked entities changes
        Task<int> SaveChangesAsync();
    }
}