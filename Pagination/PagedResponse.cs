using Microsoft.EntityFrameworkCore;

namespace HenryUtils.Pagination;

public class PagedResponse<T>
{
    public IEnumerable<T> Items { get; set; } = [];
    public int CurrentPage { get; init; }
    public int TotalPages { get; init; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public async Task<PagedResponse<T>> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return Create(items, count, pageNumber, pageSize);
    }

    public static PagedResponse<T> Create(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
    {
        return new PagedResponse<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
        };
    }
}
