namespace CarCareAlliance.Application.Common.Pagination
{
    public abstract record PagedQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
