using Chat_bot;
using ChatbotAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChatbotAPI.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ChatbotService _chatbotService;

        public ChatController(ChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage([FromBody] ChatRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest("Message is required.");
            }

            var response = await _chatbotService.GetResponseAsync(request.Message);

            return Ok(new ChatResponse
            {
                UserMessage = request.Message,
                BotResponse = response
            });
        }
    }

    public class ChatRequest
    {
        public string Message { get; set; }
    }

    public class ChatResponse
    {
        public string UserMessage { get; set; }
        public string BotResponse { get; set; }
    }
}
