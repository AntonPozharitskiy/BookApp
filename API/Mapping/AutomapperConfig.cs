using API.Requests;
using API.Responses;
using AutoMapper;
using AutoMapper.Configuration;
using BLL.Entities;

namespace API.Mapping
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
            var config = new MapperConfigurationExpression();

            config.CreateMap<RequestUserModel, User>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForAllOtherMembers(x => x.Ignore());
            config.CreateMap<User, ResponseUserModel>()
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.UserName))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForAllOtherMembers(x=>x.Ignore());
            config.CreateMap<RequestBookModel, Book>();
            config.CreateMap<Book, ResponseBookModel>();
            Mapper.Initialize(config);
            Mapper.AssertConfigurationIsValid();
        }
    }
}
