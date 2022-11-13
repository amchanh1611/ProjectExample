namespace ProjectExample.Modules.Medias.Requests
{
    public abstract class GetRequest
    {
        public string? InfoSearch { get; set; } = default!;
        public int PageSize { get; set; } = 3;
        public int CurrentPage { get; set; } = 1;
        public string? OrderBy { get; set; } = default!;
    }
}
