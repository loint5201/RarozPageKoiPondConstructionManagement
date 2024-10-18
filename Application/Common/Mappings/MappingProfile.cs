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
        }
    }
}
