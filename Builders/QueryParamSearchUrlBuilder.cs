using JobFinder.Api.Interfaces;

namespace JobFinder.Api.UrlBuilders;

public class QueryParamSearchUrlBuilder : ISearchUrlBuilder
{
    private readonly string _baseUrl;

    public QueryParamSearchUrlBuilder(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    public string Host
    {
        get
        {
            var uri = new Uri(_baseUrl);
            return $"{uri.Scheme}://{uri.Host}";
        }
    }

    public string Build(Dictionary<string, string> queryParams)
    {
        if (queryParams == null || queryParams.Count == 0)
            return _baseUrl;

        var queryString = string.Join("&",
            queryParams.Select(kvp =>
                $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));

        return $"{_baseUrl}?{queryString}";
    }
}
