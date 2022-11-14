﻿using System.Linq.Dynamic.Core;
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
            if (string.IsNullOrWhiteSpace(param))
                continue;

            string propertyNameFromQuery = param.Trim().Split(" ")[0];
            PropertyInfo propertyNameFromSourc = propertyInfos.FirstOrDefault(x => x.Name.Equals(propertyNameFromQuery, StringComparison.OrdinalIgnoreCase));

            if (propertyNameFromSourc == null)
                continue;
            if (propertyNameFromSourc.PropertyType == typeof(ICollection<>))
                continue;
            string sortingRule = param.EndsWith(" desc") ? "descending" : "ascending";

            querySortBuilder = querySortBuilder.Append($"{propertyNameFromSourc.Name.ToString()} {sortingRule}, ");
        }

        string querySort = querySortBuilder.ToString().TrimEnd(' ', ','); 


        if (string.IsNullOrWhiteSpace(querySort))
            return sourc;

        return sourc.OrderBy(querySort);
    }
}