using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WishListApi.Context;
using WishListApi.Models;

namespace WishListApi.Services
{
    public interface IWishService
    {
        public IEnumerable<WishVm> GetOwnActiveWishes(UserVm user);
    }

    public class WishService : BaseService, IWishService
    {
        public WishService(AppDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public IEnumerable<WishVm> GetOwnActiveWishes(UserVm user)
        {
            var wishes = _dbContext.Wishes
                .Include(w => w.Status)
                .Include(w => w.Visibility)
                .Include(w => w.WishContributes)
                    .ThenInclude(wc => wc.User)
                .Include(w => w.User)
                .Where(w => w.UserId == user.UserId && w.StatusId < 3)
                .ToList();
            var wishesVms = _mapper.Map<IEnumerable<WishVm>>(wishes);
            foreach (var wish in wishesVms)
            {
                decimal contributionSum = 0;
                foreach (var contribution in wish.WishContributes)
                {
                    contributionSum += contribution.Contribution;
                }
                wish.FillPercent = Decimal.Round(contributionSum / wish.Price * 100, 4);
            }

            return wishesVms;
        }
    }
}