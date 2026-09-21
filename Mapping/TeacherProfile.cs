using AutoMapper;
using SCHOOL_MANAGEMENT_API.DTO;
using SCHOOL_MANAGEMENT_API.Models;

namespace SCHOOL_MANAGEMENT_API.Mapping
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, TeacherDto>();
        }
    }
}
