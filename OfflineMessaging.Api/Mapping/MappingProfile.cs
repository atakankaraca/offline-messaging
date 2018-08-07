using AutoMapper;
using OfflineMessaging.Data;
using OfflineMessaging.Data.Model;
using OfflineMessaging.Service.DTO;

namespace OfflineMessaging.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserActivity, UserActivityDto>();
            CreateMap<UserActivityDto, UserActivity>();
            CreateMap<BlockList, BlockListDto>();
            CreateMap<BlockListDto, BlockList>();
            CreateMap<Message, MessageDto>();
            CreateMap<MessageDto, Message>();
        }
    }
}
