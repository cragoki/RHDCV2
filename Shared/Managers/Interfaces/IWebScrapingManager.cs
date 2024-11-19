using HtmlAgilityPack;
using Shared.Models;
using Shared.Models.ScrapingModels;

namespace Shared.Managers.Interfaces
{
    public interface IWebScrapingManager
    {
        URLGenerationModel GenerateNextResultRetrievalURL();
        Task AddAutoretrieverLog(DateTime date, bool success, string note, int retries = 0);
        Task<HtmlDocument> Scrape(string url);
        Task<List<ScrapingEventModel>> ScrapeAllData(HtmlDocument htmlDoc, DateTime raceDate);
    }
}