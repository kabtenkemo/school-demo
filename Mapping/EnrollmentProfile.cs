using AutoMapper;
using SCHOOL_MANAGEMENT_API.DTO;
using SCHOOL_MANAGEMENT_API.Models;

namespace SCHOOL_MANAGEMENT_API.Mapping
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Enrollment, EnrollmentDTO>()
                .ForMember(d => d.StudentName, o => o.MapFrom(s => s.Student != null ? $"{s.Student.FirstName} {s.Student.LastName}" : string.Empty))
                .ForMember(d => d.SubjectName, o => o.MapFrom(s => s.Subject != null ? s.Subject.Name : string.Empty));

            CreateMap<CreateEnrollmentDTO, Enrollment>();
            CreateMap<UpdateEnrollmentDTO, Enrollment>();
        }
    }
}
