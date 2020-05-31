using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Interface
{
    public interface IConnectionManager
    {
        void AddConnection(int userId, string ConnectionId);
        void RemoveConnection(string connectionId);
        HashSet<string> GetConnection(int email);
        IEnumerable<int> OnlineUsers { get; }
    }
}
