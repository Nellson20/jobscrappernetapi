using JobFinder.Api.Domain;
using HtmlAgilityPack;
using System.Collections.Generic;

namespace JobFinder.Api.Interfaces
{
    public interface IJobHtmlParser
    {
        /// <summary>
        /// Analyse le document HTML et retourne les jobs trouvés
        /// </summary>
        IEnumerable<Job> Parse(HtmlDocument doc, Dictionary<string, string> queryParams);
    }
}
