﻿namespace ProjectExample.Modules.Medias.Requests
{
    public abstract class SearchOrPaggingRequest
    {
        public string? InfoSearch { get; set; }
        public int PageSize { get; set; } = 2;
        public int CurrentPage { get; set; } = 1;
    }
}
