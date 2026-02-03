namespace JobFinder.Api.Domain;

public class Job
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string? Company { get; set; }
    public string? Location { get; set; }
    public List<string>? ContractTypes { get; set; }
    public List<string>? TechStack { get; set; }
    public string Source { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string? PublishedAt { get; set; }

}
