using AutoMapper;
using ProjectExample.Modules.Medias.Entities;
using ProjectExample.Modules.Medias.Requests;

namespace ProjectExample.Mapping
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            //Meida
            CreateMap<CreateMediaRequest, Media>();
        }
    }
}
