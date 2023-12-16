using AutoMapper;
using WishListApi.Models;

namespace WishListApi.Services
{
    public abstract class BaseService : IDisposable
    {
        protected readonly AppDbContext _dbContext;
        protected readonly IMapper _mapper;
        private bool _disposed;

        public BaseService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _disposed = false;
            _mapper = mapper;
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