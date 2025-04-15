﻿using System.Linq.Dynamic.Core;
namespace DevHabitApi.Services.Sorting;

internal static class QueryableExtensions
{
    public static IQueryable<T> ApplySort<T>(
        this IQueryable<T> query,
        string? sort,
        SortMapping[] mappings,
        string defaultOrderBy = "Id")
    {
        if (string.IsNullOrWhiteSpace(sort))
        {
            return query.OrderBy(defaultOrderBy);
        }

        string[] sortFields = sort.Split(',')
            .Select(s => s.Trim())
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToArray();

        var orderByParts = new List<string>();
        foreach (string field in sortFields)
        {
            (string sortField, bool isDescending) = ParseSortField(field);

            SortMapping mapping = mappings.First(m =>
                m.SortField.Equals(sortField, StringComparison.OrdinalIgnoreCase));

            string direction = (isDescending, mapping.Reverse) switch
            {
                (false, false) => "ASC",
                (false, true) => "DESC",
                (true, false) => "DESC",
                (true, true) => "ASC"
            };

            orderByParts.Add($"{mapping.PropertyName} {direction}");
        }

        string orderBy = string.Join(",", orderByParts);

        return query.OrderBy(orderBy);
    }

    private static (string SortField, bool IsDescending) ParseSortField(string field)
    {
        string[] parts = field.Split(' ');
        string sortField = parts[0];
        bool IsDescending = parts.Length > 1 &&
                            parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

        return (sortField, IsDescending);
    }
}

// this sort mapping doing by the System.Linq.Dynamic.Core package
// and the it apply OrderBy("Name ASC, Description DESC, EndDate DESC") Query
// https://dynamic-linq.net/basic-simple-query#ordering-results-combining-ascending-and-descending

