namespace JobFinder.Api.Domain;

public class SiteHtmlMapping
{
    public string SiteName { get; set; } = "";
    public string JobNodeSelector { get; set; } = ""; // le parent contenant le job
    public string TitleSelector { get; set; } = "";
    public string CompanySelector { get; set; } = "";
    public string LinkSelector { get; set; } = "";
    public string LocationSelector { get; set; } = ""; // optionnel
    public string ContractSelector { get; set; } = ""; // optionnel
    public string PublishedAtSelector { get; set; } = ""; // optionnel
}
