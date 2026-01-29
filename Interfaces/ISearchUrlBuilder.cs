namespace JobFinder.Api.Interfaces;

public interface ISearchUrlBuilder
{
    /// <summary>
    /// Construit l'URL de recherche à partir des paramètres donnés.
    /// </summary>
    /// <param name="queryParams">Dictionnaire clé/valeur pour les paramètres de recherche (ex: keyword, location, contract)</param>
    /// <returns>URL complète</returns>
    string Build(Dictionary<string, string>? queryParams);
}
