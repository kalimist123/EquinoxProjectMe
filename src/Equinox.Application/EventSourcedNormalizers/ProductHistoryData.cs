namespace Equinox.Application.EventSourcedNormalizers
{
    public class ProductHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Code { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}