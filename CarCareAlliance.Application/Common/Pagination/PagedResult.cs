namespace CarCareAlliance.Application.Common.Pagination
{
    public class PagedResult<T>(
        int pageNumber, 
        int pageSize, 
        int totalRecords, 
        IEnumerable<T> data)
    {
        public int PageNumber { get; set; } = pageNumber;
        public int PageSize { get; set; } = pageSize;
        public int TotalRecords { get; set; } = totalRecords;
        public IEnumerable<T> Data { get; set; } = data;
    }
}
