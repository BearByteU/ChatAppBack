using ChatApp.Interface;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public class NotificationHelper : INotification
    {
        IHubContext<ChatHub> _hubContext;
        private readonly IConnectionManager _connectionManager;

        public NotificationHelper(IHubContext<ChatHub> hubContext,IConnectionManager connectionManager)
        {
            _hubContext = hubContext;
            _connectionManager = connectionManager;
        }

        public IEnumerable<int> OnlineUsers()
        {
            return _connectionManager.OnlineUsers;
        }

        public async Task SendNotificationParaller(int userId,string function,object message)
        {
            HashSet<string> connections = _connectionManager.GetConnection(userId);

            if(connections!=null && connections.Count > 0)
            {
                foreach (var connection in connections)
                {
                    string con = connection.ToString();
                   await _hubContext.Clients.User(con).SendAsync(function,message);
                }
            }
        }

        public void SendNotificationToAll()
        {
           
        }
    }
}
