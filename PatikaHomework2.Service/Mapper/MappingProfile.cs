using PatikaHomework2.Data.Model;
using PatikaHomework2.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace PatikaHomework2.Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CountryDto, Country>().ReverseMap();
            CreateMap<DepartmentDto, Department>().ReverseMap();
            CreateMap<EmployeeDto, Employee>().ReverseMap();
            CreateMap<FolderDto, Folder>().ReverseMap();

        }

        
        
    }
}
