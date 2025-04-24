using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatApp.Services;
using ChatApp.Model;

namespace ChatApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Message>>> GetMessages()
        {
            var messages = await _messageService.GetMessagesAsync();
            return Ok(messages);
        }

        [HttpPost]
        public async Task<ActionResult<Message>> SendMessage([FromBody] MessageRequest request)
        {
            var message = await _messageService.SendMessageAsync(request.ReceiverId, request.ReceiverId, request.Content, request.ChatRoom);
            return Ok(message);
        }
    }

    public class MessageRequest
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }

        public string ChatRoom {get; set;}
    }
}