using System.Collections.Generic;
using System.Linq;

namespace Chatbot.Business
{
    public class TimelineUsers : ITimelineUsers
    {
        private readonly IEnumerable<string> _users;

        public TimelineUsers(IEnumerable<string> users)
        {
            _users = users;
        }

        public bool Contains(string user) => _users.Contains(user);
    }
}