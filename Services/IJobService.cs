using JobFinder.Api.Domain;

namespace JobFinder.Api.Services;

public interface IJobService
{
    Task<IEnumerable<Job>> SearchAndSaveJobAsync(string keyword, string location = "Remote", string contract = "Remote");
}
