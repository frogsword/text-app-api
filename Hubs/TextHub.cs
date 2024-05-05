using Microsoft.AspNetCore.SignalR;
using TextApp.Models;

namespace TextApp.Hubs
{
    public class TextHub : Hub
    {
        public async Task JoinGroupRoom(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }

        public async Task SendMessage(Message message)
        {
            await Clients.Group(message.GroupId.ToString()).SendAsync("ReceiveMessage", message);
        }
    }
}
