namespace BlazorAppServer.Components
{
    public class BreadcrumbItem
    {
        public string DisplayName { get; set; }
        public string Url { get; set; }
        public bool IsCurrent { get; set; }
        public int Position { get; set; }
    }
}
