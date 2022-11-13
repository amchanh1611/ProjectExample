using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace ProjectExample.Persistence.Sort;

public class SortingBase<T>
{
    public static IQueryable<T> ApplySort(IQueryable<T> sourc, string? infoSortFromQuery)
    {
        if (!sourc.Any())
            return sourc;

        if (string.IsNullOrWhiteSpace(infoSortFromQuery))
            return sourc;

        string[] sortParam = infoSortFromQuery.Trim().Split(',');
        PropertyInfo[] propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        StringBuilder querySortBuilder = new();

        foreach (string param in sortParam)
        {
            //Code mẫu sẽ có thêm cái kiểm tra param có null hay bằng khoản trắng nữa không nhưng em thấy không cần thiết vì người viết front-end sẽ truyền đúng định dạng FromQuery
            //Sao chỗ này code mẫu lại để dấu ngoặc kép mà không phải dấu ngoặc đơn
            string propertyNameFromQuery = param.Trim().Split(' ')[0];
            PropertyInfo propertyNameFromSourc = propertyInfos.FirstOrDefault(x => x.Name.Equals(propertyNameFromQuery, StringComparison.InvariantCultureIgnoreCase));

            if (propertyNameFromSourc == null)
                continue;

            string sortingRule = param.EndsWith(" desc") ? "descending" : "ascending";

            querySortBuilder = querySortBuilder.Append($"{propertyNameFromSourc.Name.ToString()} {sortingRule}, ");
        }

        string querySort = querySortBuilder.ToString().TrimEnd(' ', ',');

        //Chỗ này có cần thiết không, nếu code đã chạy xuống tới chỗ này thì chắc chắn có querySort
        if (string.IsNullOrWhiteSpace(querySort))
            return sourc;

        return sourc.OrderBy(querySort);
    }
}