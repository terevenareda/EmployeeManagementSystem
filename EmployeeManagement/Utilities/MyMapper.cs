using EmployeeManagement.DTOs;
using EmployeeManagement.Entities;
using AutoMapper;
namespace EmployeeManagement.Utilities
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));

            CreateMap<CreateEmployeeDto, Employee>();
        }
    }
}
