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
            CreateMap<CompaniesDto, Companies>();

            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();

            CreateMap<Tasks, TasksDto>();
            CreateMap<TasksDto, Tasks>();

            CreateMap<Categories, CategoriesDto>();
            CreateMap<CategoriesDto, Categories>();

            CreateMap<Types, TypesDto>();
            CreateMap<TypesDto, Types>();
        }
    }
}
