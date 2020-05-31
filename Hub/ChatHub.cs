using ChatApp.Interface;
using ChatApp.Models;
using ChatApp.SRMDbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async override Task OnConnectedAsync()
        {
           await base.OnConnectedAsync();
        }

        public async Task AddTogroup(string id)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, id);
        }

       
        public async override Task OnDisconnectedAsync(Exception exception)
        {

            //await RefreshFriendRequestList();

            await base.OnDisconnectedAsync(exception);
        }
    }
}