using HtmlAgilityPack;
using Shared.Models;
using Shared.Models.ScrapingModels;

namespace Shared.Managers.Interfaces
{
    public interface IWebScrapingManager
    {
        URLGenerationModel GenerateNextResultRetrievalURL();
        Task AddAutoretrieverLog(DateTime date, bool success, string note);
        Task<HtmlDocument> Scrape(string url);
        Task<List<EventModel>> ScrapeAllData(HtmlDocument htmlDoc, DateTime raceDate);
    }
}