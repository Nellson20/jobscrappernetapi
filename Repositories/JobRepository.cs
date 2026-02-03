using JobFinder.Api.Domain;
using JobFinder.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class JobRepository
{
    private readonly AppDbContext _context;
    public JobRepository(AppDbContext context) => _context = context;
    public async Task AddJobAsync(Job job) => _context.Jobs.Add(job);

    public async Task AddAllJobAsync(List<Job> jobs) => _context.Jobs.AddRange(jobs);

    public async Task<List<string>> FilterExistingJobsAsync(List<string> jobUrls)
    {
        return await _context.Jobs
        .Where(j => jobUrls.Contains(j.Url))
        .Select(j => j.Url)
        .ToListAsync();
    }
}