using System.Collections.Generic;
using System.Linq;
using Chatbot.Business;

namespace Chatbot.Adapters
{
    public class FollowedUsers : IFollowedUsers
    {
        private readonly IEnumerable<string> _followedUsers;

        public FollowedUsers(IEnumerable<string> followedUsers)
        {
            _followedUsers = followedUsers;
        }

        public ITimelineUsers CombineWith(string user)
        {
            var users = new List<string> { user }.Concat(_followedUsers);
            return new TimelineUsers(users);
        }
    }
}