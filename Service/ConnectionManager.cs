using ChatApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public class ConnectionManager : IConnectionManager
    {
        private static Dictionary<int, HashSet<string>> userMaps = new Dictionary<int, HashSet<string>>();

        public ConnectionManager()
        {
        }

        public IEnumerable<int> OnlineUsers => userMaps.Keys;

        public void AddConnection(int userName, string ConnectionId)
        {
            lock (userMaps)
            {
                if (!userMaps.ContainsKey(userName))
                {
                    userMaps[userName] = new HashSet<string>();
                }
                userMaps[userName].Add(ConnectionId);
            }
        }

        public HashSet<string> GetConnection(int userId)
        {
            lock (userMaps)
            {
                return userMaps.GetValueOrDefault(userId);
            }
        }

        public void RemoveConnection(string connectionId)
        {
            lock (userMaps)
            {
                foreach (var userName in userMaps.Keys)
                {
                    if (userMaps[userName].Contains(connectionId))
                    {
                        userMaps[userName].Remove(connectionId);

                        if (!userMaps[userName].Any())
                        {
                            userMaps.Remove(userName);
                        }
                        break;
                    }
                }
            }
        }
    }
}
