using ProjectExample.Modules.Medias.Entities;
using ProjectExample.Persistence.PaggingBase;

namespace ProjectExample.Modules.Medias.Response.Override
{
    public class SearchOrPaggingMediaResponse 
    {
        public SearchOrPaggingMediaResponse(List<Media> medias,PageInfo pageInfo)
        {
            Medias = medias;
            PageInfo = pageInfo;
        }
        public List<Media>? Medias { get; set; }
        public PageInfo PageInfo { get; set; } = default!;
    }
}
