namespace ProjectExample.Persistence.PaggingBase
{
    public class PaggingBase<T> : List<T>
    {
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; set; }
        public int Current { get; set; }
        public int? NextPage => Current == TotalPages ? null : Current + 1;
        public int? PreviousPage => Current == 1 ? null : Current - 1;
        public bool HasNext => Current < TotalPages;
        public bool HasPrevious => Current > 1;

        public PaggingBase(List<T> items, int count, int current, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Current = current;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static PaggingBase<T> ToPagedList(IQueryable<T> source, int current, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((current - 1) * pageSize).Take(pageSize).ToList();
            return new PaggingBase<T>(items, count, current, pageSize);
        }
    }
}