namespace BuildingBlocks.Pagination;

/// <summary>
/// Pagination result object
/// </summary>
/// <param name="pageIndex">Index of the page</param>
/// <param name="pageSize">Number of items per page</param>
/// <param name="count">Total items</param>
/// <param name="data">Actual data</param>
/// <typeparam name="TEntity"></typeparam>
public class PaginatedResult<TEntity> (int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
    where TEntity : class
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public long Count { get; } = count;
    public IEnumerable<TEntity> Data { get; } = data;
}