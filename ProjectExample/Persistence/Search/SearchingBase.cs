using System.Linq.Expressions;

namespace ProjectExample.Persistence.SearchBase
{
    public class SearchingBase<T>
    {
        public static IQueryable<T> ApplySearch(IQueryable<T> sourc,Expression<Func<T, bool>> expression)
        {
            return sourc.Where(expression);
        }
    }
}
