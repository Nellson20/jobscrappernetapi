using JobFinder.Api.Domain;

namespace JobFinder.Api.Services;

public interface IJobScrapperService
{
    IEnumerable<Job> ScrapeJobs(string? keyword = null);
}
