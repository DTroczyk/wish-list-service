using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WishListApi.Context;
using WishListApi.Models;

namespace WishListApi.Services
{
    public class WishService : BaseService, IWishService
    {
        public WishService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        // public IEnumerable<Wish> GetOwnWishes()
        // {
        //     throw new NotImplementedException();
        // }
    }
}