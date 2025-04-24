using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Model;
using ChatApp.Services;

namespace ChatApp.Services
{
    public interface IMessageService
    {
        Task<List<Message>> GetMessagesAsync();
        Task<Message> SendMessageAsync(string senderId, string receiverId, string content, string chatRoom);
       // Task SendMessageAsync(string senderId, string receiverId, string content, string chatRoom);
        Task SendMessageAsync(string senderId, string receiverId, string content);
    }
}