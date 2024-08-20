using BookStoreManagement.Domain.Context;

namespace BookStoreManagement.Service.Repository
{
    public class BookStoreRepository : IBookStoreRepository
    {
        #region Properties

        public BookStoreDBContext DbContext { get; set; }

        #endregion Properties

        #region Constructors

        public BookStoreRepository(BookStoreDBContext dbContext)
        {
            DbContext = dbContext;
        }

        #endregion Constructors

        #region Methods

        public virtual T Add<T>(T entity) where T : class
        {
            return DbContext.Set<T>().Add(entity).Entity;
        }

        public virtual T Remove<T>(T entity) where T : class
        {
            return DbContext.Set<T>().Remove(entity).Entity;
        }

        public virtual void RemoveRange<T>(List<T> entities) where T : class
        {
            DbContext.RemoveRange(entities);
        }

        public virtual void AddRange<T>(List<T> entities) where T : class
        {
            DbContext.AddRange(entities);
        }

        public virtual IQueryable<T> GetAll<T>() where T : class
        {
            return DbContext.Set<T>().AsQueryable();
        }

        public Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        #endregion Methods
    }
}