
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
    {
        // Maps usernames to connection IDs
        private static ConcurrentDictionary<string, string> ConnectedUsers = new();

        public override Task OnConnectedAsync()
        {
            var username = Context.GetHttpContext()?.Request.Query["username"].ToString();
            if (!string.IsNullOrWhiteSpace(username))
            {
                ConnectedUsers[username] = Context.ConnectionId;
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var user = ConnectedUsers.FirstOrDefault(kvp => kvp.Value == Context.ConnectionId).Key;
            if (!string.IsNullOrEmpty(user))
            {
                ConnectedUsers.TryRemove(user, out _);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendPrivateMessage(string fromUser, string toUser, string message)
        {
            if (ConnectedUsers.TryGetValue(toUser, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceivePrivateMessage", fromUser, message);
                await Clients.Caller.SendAsync("ReceivePrivateMessage", fromUser, message); // echo back
            }
        }

        public async Task GetOnlineUsers()
        {
            var userList = ConnectedUsers.Keys.ToList();
            await Clients.Caller.SendAsync("OnlineUsers", userList);
        }
    }
}
