namespace CarCareAlliance.Presentation.Client.Models
{
    public class PaginatedList<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; } 
        public int TotalRecords { get; set; }
        public List<T> Data { get; set; } = [];
    }
}
