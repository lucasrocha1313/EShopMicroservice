namespace BuildingBlocks.Pagination;

/// <summary>
/// Pagination request object
/// </summary>
/// <param name="PageNumber">Index of the page. Should start from 1</param>
/// <param name="PageSize">Size of the page. Default is 10</param>
public record PaginationRequest(int PageNumber = 1, int PageSize = 10);