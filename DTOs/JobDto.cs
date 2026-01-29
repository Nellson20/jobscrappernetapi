namespace JobFinder.Api.DTOs;

public class JobDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public List<string> ContractTypes { get; set; } = new();
    public List<string> TechStack { get; set; } = new();
    public string Source { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string PublishedAt { get; set; } = "";
}
