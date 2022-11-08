using ProjectExample.Modules.Medias.Entities;

namespace ProjectExample.Modules.Medias.Response.Override
{
    public class SearchOrPaggingMediaResponse : PageInfo
    {
        public List<Media>? Medias { get; set; }
    }
}
