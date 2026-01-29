using JobFinder.Api.Domain;

namespace JobFinder.Api.Interfaces;

public interface IJobScrapperService
{
    IEnumerable<Job> ScrapeJobs(
        ISearchUrlBuilder urlBuilder,
        Dictionary<string, string>? queryParams,
        SiteHtmlMapping mapping
    );
}
