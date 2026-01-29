using HtmlAgilityPack;
using JobFinder.Api.Domain;
namespace JobFinder.Api.Services;

public class JobService : IJobService
{
    private readonly IJobScrapperService _scraper;

    public JobService(IJobScrapperService scraper)
    {
        _scraper = scraper;
    }

    public IEnumerable<Job> Search(string? keyword = null)
    {
        // 1️⃣ Scraper les jobs
        var scrapedJobs = _scraper.ScrapeJobs(keyword);

        // 2️⃣ Ajouter éventuellement des mocks internes
        var mockJobs = new List<Job>
        {
            new Job
            {
                Title = "Développeur Fullstack React / Symfony",
                Company = "Startup Africa",
                Location = "Remote",
                ContractType = "CDI",
                TechStack = new() { "React", "Symfony", "PostgreSQL" },
                Source = "Mock",
                Url = "https://example.com",
                PublishedAt = DateTime.UtcNow
            }
        };

        return mockJobs.Concat(scrapedJobs);

    }
}

