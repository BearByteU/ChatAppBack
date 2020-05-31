using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Interface
{
    public interface INotification
    {
        void SendNotificationToAll();

        IEnumerable<int> OnlineUsers();

        Task SendNotificationParaller(int userId, string function, object messageDto);

    }
}
