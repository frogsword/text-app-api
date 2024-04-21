using Microsoft.AspNetCore.SignalR;

namespace TextApp.Hubs
{
    public class TextHub : Hub
    {
        public async Task SendMessage(string sender, string receiver, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", sender, receiver, message);
        }
    }
}
