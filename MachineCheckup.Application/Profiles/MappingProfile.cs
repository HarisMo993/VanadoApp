using AutoMapper;
using MachineCheckup.Application.Dtos;
using MachineCheckup.Domain.Entities;

namespace MachineCheckup.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Machine
            CreateMap<Machine, MachineDto>().ReverseMap();
            CreateMap<Machine, CreateMachineDto>().ReverseMap();
            #endregion

            #region Issue
            CreateMap<Issue, IssueDto>().ReverseMap();
            CreateMap<Issue, CreateIssueDto>().ReverseMap();
            #endregion
        }
    }
}
