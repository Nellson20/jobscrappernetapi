using HtmlAgilityPack;
using JobFinder.Api.Domain;
using JobFinder.Api.Interfaces;
using JobFinder.Api.UrlBuilders;
namespace JobFinder.Api.Services;

public class JobService : IJobService
{
    private readonly IJobScrapperService _scrapperService;

    public JobService(IJobScrapperService scrapperService)
    {
        _scrapperService = scrapperService;
    }

    public IEnumerable<Job> Search(string keyword, string location = "Remote", string contract = "Remote")
    {
        var weWorkBuilderParams = new Dictionary<string, string>
        {
            { "term", keyword },
            { "location", location },
            { "contract", contract }
        };

        var weWorkMapping = new SiteHtmlMapping
        {
            SiteName = "WeWorkRemotely",
            JobNodeSelector = "//section[@class='jobs']//li",
            TitleSelector = ".//h3[@class='new-listing__header__title']",
            CompanySelector = ".//p[@class='new-listing__company-name']",
            LinkSelector = ".//a[@class='listing-link--unlocked']",
            ContractSelector = ".//p[@class='new-listing__categories__category']",
            LocationSelector = ".//p[@class='new-listing__company-headquarters']", // optionnel
            PublishedAtSelector = ".//p[@class='new-listing__header__icons__date']" // optionnel
        };

        // 2️⃣ Ajouter éventuellement des mocks internes
        // On peut scraper plusieurs sites et concaténer les résultats
        var allJobs = new List<Job>();

        // Exemple site 1
        var weWorkBuilder = new QueryParamSearchUrlBuilder("https://weworkremotely.com/remote-jobs/search");
        allJobs.AddRange(_scrapperService.ScrapeJobs(weWorkBuilder, weWorkBuilderParams, weWorkMapping));

        // // Exemple site 2
        // var remoteOkBuilder = new QueryParamSearchUrlBuilder("https://remoteok.com/remote-dev-jobs");
        // allJobs.AddRange(_scrapperService.ScrapeJobs(remoteOkBuilder, queryParams));

        // Ici tu pourrais trier, filtrer, enlever les doublons, etc.
        // .OrderByDescending(j => j.PublishedAt)
        return allJobs;

    }
}

