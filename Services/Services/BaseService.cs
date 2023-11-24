using WishListApi.Models;

namespace WishListApi.Services
{
    public abstract class BaseService : IDisposable
    {
        protected readonly AppDbContext _dbContext;
        private bool _disposed;

        public BaseService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _disposed = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _dbContext.Dispose();
            _disposed = true;
        }
    }
}