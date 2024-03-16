namespace CarCareAlliance.Presentation.Client.Common.Auth
{
    public class HttpErrorResponse
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Status { get; set; }
        public Dictionary<string, string[]> Errors { get; set; } = [];
        public string TraceId { get; set; } = string.Empty;
    }
}