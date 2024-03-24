namespace CarCareAlliance.Presentation.Client.Models
{
    public class PaginatedList<T>
    {
        public int TotalRecords { get; set; }
        public List<T> Data { get; set; } = [];
    }
}
