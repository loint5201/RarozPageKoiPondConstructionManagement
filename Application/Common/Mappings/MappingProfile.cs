using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ConstructionRequestDTO, ConstructionRequest>()
                .ReverseMap();

            CreateMap<ConstructionProcessResponse, ConstructionProcess>()
                .ReverseMap();

            CreateMap<ConstructionProcessRequest, ConstructionProcess>()
                .ReverseMap();

            CreateMap<RegisterRequest, User>()
               .ForMember(dest => dest.Avatar, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<UserResponse, User>()
               .ReverseMap();

            CreateMap<MaintenanceServiceDTO, MaintenanceService>()
                .ForMember(dest => dest.ServiceImage, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
