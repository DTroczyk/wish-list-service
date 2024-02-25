using AutoMapper;
using WishListApi.Context;
using WishListApi.Models;

namespace WishListApi.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<User, UserVm>();
        }
    }
}