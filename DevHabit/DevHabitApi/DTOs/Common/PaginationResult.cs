using Microsoft.EntityFrameworkCore;

namespace DevHabitApi.DTOs.Common;

public sealed record PaginationResult<T> : ICollectionresponse<T>, ILinksResponse
{
    public List<T> Items { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public List<LinkDto> Links { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;

    public static async Task<PaginationResult<T>> CreateAsync(
    IQueryable<T> query,
    int page,
    int pageSize)
    {
        int totalCount = await query.CountAsync();
        List<T> items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginationResult<T>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}

//if total 12 page = 3 and pagesize = 4 should skip 8 items and take 4
