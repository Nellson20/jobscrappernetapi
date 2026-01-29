using System.Text.Json;
using HtmlAgilityPack;
using JobFinder.Api.Domain;

namespace JobFinder.Api.Services;

public class JobScrapperService : IJobScrapperService
{
    public IEnumerable<Job> ScrapeJobs(string? keyword = null)
    {
        var jobs = new List<Job>();

        var web = new HtmlWeb();
        var url = "https://weworkremotely.com/categories/remote-programming-jobs";
        var doc = web.Load(url);

        // Chaque job est dans li class="feature" ou li class=""
        foreach (var node in doc.DocumentNode.SelectNodes("//section[@class='jobs']//li") ?? Enumerable.Empty<HtmlNode>())
        {
            var titleNode = node.SelectSingleNode(".//h3[@class='new-listing__header__title']");
            var companyNode = node.SelectSingleNode(".//p[@class='new-listing__company-name']");
            var linkNode = node.SelectSingleNode(".//a[@class='listing-link--unlocked']");

            if (titleNode == null || companyNode == null || linkNode == null) continue;

            var title = titleNode.InnerText.Trim() ?? "n/a";
            var company = companyNode.InnerText.Trim() ?? "n/a";
            var jobUrl = "https://weworkremotely.com" + linkNode.GetAttributeValue("href", "#");

            jobs.Add(new Job
            {
                Title = title,
                Company = company,
                Location = "Remote",
                ContractType = "Remote",
                TechStack = new List<string>(),
                Source = "WeWorkRemotely",
                Url = jobUrl,
                PublishedAt = DateTime.UtcNow
            });
        }

        // Filtrage keyword
        if (!string.IsNullOrEmpty(keyword))
        {
            jobs = jobs.Where(j => j.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                   j.Company.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return jobs;

    }
}
