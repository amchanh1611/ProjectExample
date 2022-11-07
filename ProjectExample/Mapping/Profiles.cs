using AutoMapper;
using ProjectExample.Modules.Medias.Entities;
using ProjectExample.Modules.Medias.Requests;
using ProjectExample.Modules.Medias.Response;

namespace ProjectExample.Mapping
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            //Meida
            CreateMap<CreateMediaRequest, Media>();
            CreateMap<UpdateMediaRequest, Media>();
            CreateMap<Media,ComboboxMedia>();

            //Schedule
            CreateMap<CreateScheduleRequest, Schedule>();
            CreateMap<UpdateScheduleRequest, Schedule>();
            CreateMap<Schedule, ScheduleInDayResponse>();
        }
    }
}
