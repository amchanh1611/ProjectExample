using AutoMapper;
using ProjectExample.Modules.Medias.Entities;
using ProjectExample.Modules.Medias.Requests;
using ProjectExample.Modules.Medias.Response;
using ProjectExample.Modules.Medias.Response.Override;
using ProjectExample.Persistence.PaggingBase;

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
            CreateMap<PaggingBase<Media>, SearchOrPaggingMediaResponse>();

            //Schedule
            CreateMap<CreateScheduleRequest, Schedule>();
            CreateMap<UpdateScheduleRequest, Schedule>();
            CreateMap<Schedule, ScheduleInDayResponse>();
            CreateMap<Schedule, ScheduleResponse>();
            CreateMap<PaggingBase<Schedule>, SearchOrPagingScheduleResponse>();
        }
    }
}
