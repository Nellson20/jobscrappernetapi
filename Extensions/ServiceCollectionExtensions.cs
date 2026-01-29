using JobFinder.Api.Domain;
using JobFinder.Api.Interfaces;
using JobFinder.Api.Services;
using JobFinder.Api.UrlBuilders;

namespace JobFinder.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJobFinderServices(this IServiceCollection services)
    {
        // Liste des sites
        var siteMappings = new Dictionary<string, SiteHtmlMapping>
        {
            ["WeWorkRemotely"] = new SiteHtmlMapping
            {
                SiteName = "WeWorkRemotely",
                JobNodeSelector = "//section[@class='jobs']//li",
                TitleSelector = ".//h3[@class='new-listing__header__title']",
                CompanySelector = ".//p[@class='new-listing__company-name']",
                LinkSelector = ".//a[@class='listing-link--unlocked']"
            }
        };

        services.AddSingleton(siteMappings);

        services.AddScoped<IJobScrapperService, JobScrapperService>();
        services.AddScoped<IJobService, JobService>();

        return services;
    }
}
