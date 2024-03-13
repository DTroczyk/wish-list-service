using AutoMapper;
using WishListApi.Context;
using WishListApi.Models;

namespace WishListApi.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserVm>();
            CreateMap<Wish, WishVm>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Status));
            CreateMap<WishContribute, WishContributeVm>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
        }
    }
}