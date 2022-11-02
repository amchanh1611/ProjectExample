using ProjectExample.Modules.Medias.Entities;

namespace ProjectExample.Modules.Medias.Requests
{
    public class UpdateMediaRequest : CreateOrUpdateMediaRequest
    {
        public MediaStatus? Status { get; set; }
    }
}
