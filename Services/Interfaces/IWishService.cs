using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishListApi.Models;

namespace WishListApi.Services
{
    public interface IWishService
    {
        public IEnumerable<Wish> GetOwnWishes();
    }
}