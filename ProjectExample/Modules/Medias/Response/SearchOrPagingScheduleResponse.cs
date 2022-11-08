using ProjectExample.Modules.Medias.Entities;

namespace ProjectExample.Modules.Medias.Response
{
    public class SearchOrPagingScheduleResponse : PageInfo
    {
        public List<Schedule> Schedules { get; set; } = default!;
    }
}
