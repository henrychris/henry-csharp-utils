namespace HenryUtils.Pagination;

/// <summary>
/// This class has default values for page number and page size.
/// The default values are set to 1 for page number and 20 for page size.
/// If the page number is set to a value less than 1, it defaults to 1.
/// If the page size is set to a value less than 1, it defaults to 20.
/// The max page size is not limited.
/// </summary>
public abstract class PaginationParameters
{
    const int MIN_PAGE_NUMBER = 1;
    const int MIN_PAGE_SIZE = 20;

    private int _pageSize = MIN_PAGE_SIZE;
    private int _pageNumber = MIN_PAGE_NUMBER;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value >= MIN_PAGE_NUMBER ? value : MIN_PAGE_NUMBER;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 0 ? value : MIN_PAGE_SIZE;
    }
}
