using AutoMapper;
using SCHOOL_MANAGEMENT_API.DTO;
using SCHOOL_MANAGEMENT_API.Models;

namespace SCHOOL_MANAGEMENT_API.Mapping
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department,DepartmentDTO>();
            CreateMap<Department, CreateDepDTO>();
            CreateMap<Department, UpdateDepDTO>();

        }
    }
}
