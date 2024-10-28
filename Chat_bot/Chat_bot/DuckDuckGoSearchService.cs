using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatbotAPI.Services
{
    public class DuckDuckGoSearchService
    {
        private readonly HttpClient _httpClient;

        public DuckDuckGoSearchService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetSearchResultAsync(string query)
        {
            // Format the search URL for DuckDuckGo
            var requestUrl = $"https://html.duckduckgo.com/html/?q={Uri.EscapeDataString(query)}";

            // Send the GET request
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
            {
                return "Failed to retrieve search results.";
            }

            // Load the HTML content
            var pageContent = await response.Content.ReadAsStringAsync();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(pageContent);

            // Parse the HTML to find the first result
            var resultNode = htmlDoc.DocumentNode
                .SelectNodes("//a[@class='result__a']")
                ?.FirstOrDefault();
            var descriptionNode = htmlDoc.DocumentNode
                .SelectNodes("//a[@class='result__snippet']")
                ?.FirstOrDefault();
            if (resultNode == null || descriptionNode ==null)
            {
                return "No search results found.";
            }

            //var title = resultNode.InnerText;
            var description = descriptionNode.InnerText;
            return $"\nDescription: {description}";
        }
    }
}
