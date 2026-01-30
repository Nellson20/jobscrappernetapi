using System.Net.Http;
using HtmlAgilityPack;
using JobFinder.Api.Domain;
using JobFinder.Api.Interfaces;

namespace JobFinder.Api.Services;

public class JobScrapperService : IJobScrapperService
{

    private string GetFullUrl(string href, string siteHost)
    {
        if (string.IsNullOrWhiteSpace(href))
            return siteHost;

        href = href.Trim();

        // Si l'URL est déjà absolue
        if (href.StartsWith("http://") || href.StartsWith("https://"))
            return href;

        // Si le siteHost n'a pas de slash final, on l'ajoute
        if (!siteHost.EndsWith("/"))
            siteHost += "/";

        // Si href commence par /, on l'enlève pour éviter "//"
        if (href.StartsWith("/"))
            href = href.Substring(1);

        return siteHost + href;
    }

    public IEnumerable<Job> ScrapeJobs(ISearchUrlBuilder urlBuilder, 
                                   Dictionary<string, string>? queryParams,
                                   SiteHtmlMapping mapping)
    {
        var jobs = new List<Job>();

        var url = urlBuilder.Build(queryParams);

        var host = (urlBuilder as dynamic)?.Host ?? "";

        var web = new HtmlWeb();
        var doc = web.Load(url);

        foreach (var node in doc.DocumentNode.SelectNodes(mapping.JobNodeSelector) 
                                    ?? Enumerable.Empty<HtmlNode>())
        {
            var titleNode = node.SelectSingleNode(mapping.TitleSelector);
            var companyNode = node.SelectSingleNode(mapping.CompanySelector);
            var linkNode = node.SelectSingleNode(mapping.LinkSelector);
            var PublishedAtNode = node.SelectSingleNode(mapping.PublishedAtSelector);

            var contracts = new List<string>();
            
            var contractNodes = node.SelectNodes(mapping.ContractSelector);

            if (contractNodes != null)
            {
                foreach (var contractNode in contractNodes)
                {
                    contracts.Add(contractNode.InnerText.Trim());
                }
            }

            if (titleNode == null || companyNode == null || linkNode == null)
                continue;

            var title = titleNode.InnerText.Trim();
            var company = companyNode.InnerText.Trim();
            var jobUrl = GetFullUrl(linkNode.GetAttributeValue("href", "#"), host);

            var location = !string.IsNullOrWhiteSpace(mapping.LocationSelector)
                ? node.SelectSingleNode(mapping.LocationSelector)?.InnerText.Trim() ?? "Remote"
                : "Remote";

            var publishedAt = PublishedAtNode?.InnerText.Trim() ?? "";


            jobs.Add(new Job
            {
                Title = title,
                Company = company,
                Url = jobUrl,
                Location = location,
                ContractTypes = contracts,
                Source = host,
                TechStack = new List<string>(),
                PublishedAt = publishedAt
            });
        }

        return jobs;
    }
}
