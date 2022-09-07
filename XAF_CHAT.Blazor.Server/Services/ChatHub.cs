using Microsoft.AspNetCore.SignalR;
using XAF_CHAT.Module.BusinessObjects;

namespace XAF_CHAT.Blazor.Server.Services
{
    public class ChatHub : Hub
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(string message, string userName)
        {
            await Clients.All.SendAsync("ReceiveMessage", message, userName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="receiverUserId"></param>
        /// <param name="senderUserId"></param>
        /// <returns></returns>
        public async Task ChatNotificationAsync(string message, string receiverUserId, string senderUserId)
        {
            await Clients.All.SendAsync("ReceiveChatNotification", message, receiverUserId, senderUserId);
        }
    }
}
