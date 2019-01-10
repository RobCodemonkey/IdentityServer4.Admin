using AutoMapper;
using Code4.IdentityServer4.Admin.BusinessLogic.Dtos.Log;
using Code4.IdentityServer4.Admin.BusinessLogic.Shared.Dtos.Common;
using Code4.IdentityServer4.Admin.EntityFramework.Entities;

namespace Code4.IdentityServer4.Admin.BusinessLogic.Mappers
{
    public class LogMapperProfile : Profile
    {
        public LogMapperProfile()
        {
            CreateMap<Log, LogDto>(MemberList.Destination)
                .ReverseMap();
            
            CreateMap<PagedList<Log>, LogsDto>(MemberList.Destination)
                .ForMember(x => x.Logs, opt => opt.MapFrom(src => src.Data));
        }
    }
}
