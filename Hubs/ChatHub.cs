using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{


public class ChatHub : Hub
{
    private static readonly Dictionary<string, string> UserConnections = new();

    public override async Task OnConnectedAsync()
    {
        string connectionId = Context.ConnectionId;
        Console.WriteLine($"User connected: {connectionId}");
        await Clients.Caller.SendAsync("Connected", connectionId);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        string connectionId = Context.ConnectionId;
        Console.WriteLine($"User disconnected: {connectionId}");
        UserConnections.Remove(connectionId);
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(string user, string message, string chatRoom = "General")
    {
        Console.WriteLine($"Message from {user}: {message} (Room: {chatRoom})");
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task JoinRoom(string chatRoom)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatRoom);
        await Clients.Group(chatRoom).SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} joined {chatRoom}");
    }

    public async Task LeaveRoom(string chatRoom)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoom);
        await Clients.Group(chatRoom).SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} left {chatRoom}");
    }
}
}
