namespace CarCareAlliance.Presentation.Common.Helpers
{
    public class PaginationFilter
    {
        private const int PAGE_SIZE = 10;
        private const int PAGE_NUMBER = 1;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationFilter()
        {
            PageNumber = PAGE_NUMBER;
            PageSize = PAGE_SIZE;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < PAGE_NUMBER ? PAGE_NUMBER : pageNumber;
            PageSize = pageSize < 1 ? PAGE_SIZE : pageSize;
        }
    }
}
