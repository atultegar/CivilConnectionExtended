namespace CivilConnection.ApiExtractor.Models
{
    public class NamespaceInfo
    {
        public string Name { get; set; } = string.Empty;
        public List<ClassInfo> Classes { get; set; } = new();
    }
}
