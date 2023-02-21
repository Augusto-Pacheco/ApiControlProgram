using ApiControlProgram.Dto;
using ApiControlProgram.Model;
using AutoMapper;

namespace ApiControlProgram.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Companies, CompaniesDto>();
            CreateMap<Project, ProjectDto>();
        }
    }
}
