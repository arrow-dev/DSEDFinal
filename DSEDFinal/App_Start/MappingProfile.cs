using AutoMapper;
using DSEDFinal.Dtos;
using DSEDFinal.Models;

namespace DSEDFinal.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Job, JobDto>();
            CreateMap<Hazard, HazardDto>();
            CreateMap<Notification, NotificationDto>();
        }
    }
}