namespace BuildingBlocks.Pagination;

/// <summary>
/// Pagination result object
/// </summary>
/// <param name="pageIndex">Index of the page</param>
/// <param name="pageSize">Number of items per page</param>
/// <param name="count">Total items</param>
/// <param name="data">Actual data</param>
/// <typeparam name="TEntity"></typeparam>
public class PaginationResult<TEntity> (int pageIndex, int pageSize, int count, IEnumerable<TEntity> data)
    where TEntity : class
{
    public int PageIndex { get; } = pageIndex;
    public int PageSize { get; } = pageSize;
    public int Count { get; } = count;
    public IEnumerable<TEntity> Data { get; } = data;
}