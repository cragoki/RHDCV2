using HtmlAgilityPack;
using Shared.Models;

namespace Shared.Managers.Interfaces
{
    public interface IWebScrapingManager
    {
        URLGenerationModel GenerateNextResultRetrievalURL();
        Task AddAutoretrieverLog(DateTime date, bool success);
        Task<HtmlDocument> Scrape(string url);
        Task ScrapeAllData(HtmlDocument htmlDoc, DateTime raceDate);
    }
}