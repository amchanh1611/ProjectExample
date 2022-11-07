namespace ProjectExample.Modules.Medias.Response
{
    public class PageInfo
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int Current { get; set; }
        public int TotalPages { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
    }
}
