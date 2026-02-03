using HtmlAgilityPack;
using JobFinder.Api.Domain;
using JobFinder.Api.Interfaces;
using JobFinder.Api.UrlBuilders;
namespace JobFinder.Api.Services;

public class JobService : IJobService
{
    private readonly IJobScrapperService _scrapperService;

    private readonly JobRepository _jobRepository;

    public JobService(IJobScrapperService scrapperService, JobRepository jobRepository)
    {
        _scrapperService = scrapperService;
        _jobRepository = jobRepository;
    }

    public async Task<IEnumerable<Job>> SearchAndSaveJobAsync(string keyword, string location = "Remote", string contract = "Remote")
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

        var allJobs = new List<Job>();

        // Exemple site 1
        var weWorkBuilder = new QueryParamSearchUrlBuilder("https://weworkremotely.com/remote-jobs/search");
        allJobs.AddRange(_scrapperService.ScrapeJobs(weWorkBuilder, weWorkBuilderParams, weWorkMapping));

        var scrapedUrls = allJobs.Select(j => j.Url).ToList();

        var existingUrls = await _jobRepository.FilterExistingJobsAsync(scrapedUrls);


        var newJobs = allJobs
        .Where(j => !existingUrls.Contains(j.Url))
        .ToList();
        
        if (newJobs.Any())
        {
            await _jobRepository.AddAllJobAsync(newJobs);
        }

        return newJobs;
        // // Exemple site 2
        // var remoteOkBuilder = new QueryParamSearchUrlBuilder("https://remoteok.com/remote-dev-jobs");
        // allJobs.AddRange(_scrapperService.ScrapeJobs(remoteOkBuilder, queryParams));

        // Ici tu pourrais trier, filtrer, enlever les doublons, etc.
        // .OrderByDescending(j => j.PublishedAt)

    }
}

