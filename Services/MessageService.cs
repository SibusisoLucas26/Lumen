using System.Collections.Generic;
using System.Threading.Tasks;
using ChatApp.Data;
using ChatApp.Model;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;

        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetMessagesAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<Message> SendMessageAsync(string senderId, string receiverId, string chatRoom , string content)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                ChatRoom = chatRoom,
                Timestamp = DateTime.UtcNow
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return message;
        }

        public Task<Message> SendMessageAsync(string senderId, string receiverId, string content)
        {
            throw new NotImplementedException();
        }

       

        Task IMessageService.SendMessageAsync(string senderId, string receiverId, string content)
        {
            throw new NotImplementedException();
        }
    }
}