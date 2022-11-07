namespace ProjectExample.Modules.Medias.Requests
{
    public class SearchOrPagingScheduleRequest
    {
        public string? InfoSearch { get; set; }
        public int? PageSize { get; set; }
        public int? CurrentPage { get; set; }

    }
}
