using AutoMapper;
using SCHOOL_MANAGEMENT_API.DTO;
using SCHOOL_MANAGEMENT_API.Models;

namespace SCHOOL_MANAGEMENT_API.Mapping
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDTO>()
                .ForMember(des=>des.FullName,opt=>opt.MapFrom(src=>$"{src.FirstName} {src.LastName}"));

            CreateMap<StudentDTO, Student>()
                .ForMember(des => des.FirstName, opt => opt.MapFrom(src => src.FullName.Split(' ')[0]))
                .ForMember(des => des.LastName , opt => opt.MapFrom(src => src.FullName.Split(' ')[1]));

            CreateMap<DTO.CreateStudentDTO, Student>()
                .ForMember(des => des.FirstName, opt => opt.MapFrom(src => src.FullName.Split(' ')[0]))
                .ForMember(des => des.LastName, opt => opt.MapFrom(src => src.FullName.Split(' ')[1]));

            CreateMap<DTO.UpdateStudentDTO, Student>()
                .ForMember(des => des.FirstName, opt => opt.MapFrom(src => src.FullName.Split(' ')[0]))
                .ForMember(des => des.LastName, opt => opt.MapFrom(src => src.FullName.Split(' ')[1]));


        
        }
    }
}
