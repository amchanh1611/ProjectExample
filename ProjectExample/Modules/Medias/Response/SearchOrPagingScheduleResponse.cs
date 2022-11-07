namespace ProjectExample.Modules.Medias.Response
{
    public class SearchOrPagingScheduleResponse
    {
        List<ScheduleResponse> Schedules { get; set; } = default!;
        PageInfo PageInfo { get; set; }
    }
}
