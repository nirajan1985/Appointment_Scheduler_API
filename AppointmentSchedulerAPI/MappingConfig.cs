using AppointmentSchedulerAPI.Models;
using AppointmentSchedulerAPI.Models.Dto;
using AutoMapper;

namespace AppointmentSchedulerAPI
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentCreateDTO>().ReverseMap();
            CreateMap<Appointment,AppointmentUpdateDTO>().ReverseMap();

            CreateMap<AppointmentCategory, AppointmentCategoryDTO>().ReverseMap();
            CreateMap<AppointmentCategory, AppointmentCategoryCreateDTO>().ReverseMap();
            CreateMap<AppointmentCategory, AppointmentCategoryUpdateDTO>().ReverseMap();
        }
    }
}
