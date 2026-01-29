using JobFinder.Api.Domain;

namespace JobFinder.Api.Services;

public interface IJobService
{
    IEnumerable<Job> Search(string keyword, string location = "Remote", string contract = "Remote");
}
