using AutoMapper;
using SCHOOL_MANAGEMENT_API.DTO;
using SCHOOL_MANAGEMENT_API.Models;

namespace SCHOOL_MANAGEMENT_API.Mapping
{
    public class ClassRoomProfile : Profile
    {
        public ClassRoomProfile()
        {
            CreateMap<ClassRoom, ClassRoomDTO>();
            CreateMap<CreateClassRoomDTO, ClassRoom>();
            CreateMap<UpdateClassRoomDTO, ClassRoom>();
        }
    }
}
