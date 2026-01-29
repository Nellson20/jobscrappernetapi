namespace JobFinder.Api.Interfaces
{
    public interface IHtmlMapper
    {
        /// <summary>
        /// XPath pour sélectionner chaque nœud job dans la page
        /// </summary>
        string JobNodeXPath { get; }

        /// <summary>
        /// XPath pour récupérer le titre du job depuis un nœud
        /// </summary>
        string TitleXPath { get; }

        /// <summary>
        /// XPath pour récupérer le nom de l'entreprise depuis un nœud
        /// </summary>
        string CompanyXPath { get; }

        /// <summary>
        /// XPath pour récupérer le lien du job depuis un nœud
        /// </summary>
        string LinkXPath { get; }

        /// <summary>
        /// Optionnel : location, contrat, tech stack, etc. peuvent être ajoutés plus tard
        /// </summary>
    }
}
