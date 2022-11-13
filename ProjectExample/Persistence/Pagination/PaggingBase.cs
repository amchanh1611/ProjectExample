namespace ProjectExample.Persistence.PaggingBase
{
    public class PaggingBase<T>
    {
        public static PaggingResponse<T> ApplyPaging(IQueryable<T> source, int current, int pageSize)
        {
            int count = source.Count();
            List<T> items = source.Skip((current - 1) * pageSize).Take(pageSize).ToList();
            return new PaggingResponse<T>(items, new PageInfo(count, pageSize, current));
        }
    }

    public class PageInfo
    {
        public PageInfo(int totalCount, int pageSize, int current)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            Current = current;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            //TotalPages = TotalCount / pageSize + (TotalCount % pageSize > 0 ? 1 : 0);
        }

        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; set; }
        public int Current { get; set; }
        public int? NextPage => Current >= TotalPages ? null : Current + 1;
        public int? PreviousPage => Current <= 1 ? null : Current - 1;
        public bool HasNext => NextPage != null;
        public bool HasPrevious => PreviousPage != null;
    }

    public class PaggingResponse<T>
    {
        public PaggingResponse(List<T> data, PageInfo pageInfo)
        {
            Data = data;
            PageInfo = pageInfo;
        }

        public List<T> Data { get; set; } = default!;
        public PageInfo PageInfo { get; set; } = default!;
    }
}