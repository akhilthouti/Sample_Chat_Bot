using ChatbotAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Chat_bot
{
    public class ChatbotService
    {
        private Dictionary<string, string> _responses;
        private readonly DuckDuckGoSearchService _searchService;

        public ChatbotService(DuckDuckGoSearchService searchService)
        {
            // Define chatbot responses
            _searchService = searchService;
            _responses = new Dictionary<string, string>
            {
                { "hello", "Hello! How can I help you today?" },
                { "how are you", "I'm just a bot, but I'm functioning as expected!" },
                { "help", "I can assist you with information or answer questions." },
                { "bye", "Goodbye! Have a great day!" },
                { "brian", "Mera Yesu Yesu , Mera Yesu Yesu" }
            };
        }

        public async Task<string> GetResponseAsync(string input)
        {
            input = input.Trim();

            // Check if the input matches any predefined response
            if (_responses.ContainsKey(input))
            {
                return _responses[input];
            }
            else
            {
                return await _searchService.GetSearchResultAsync(input);
            }
        }
    }
}
