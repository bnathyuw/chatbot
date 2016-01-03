using System.Collections.Generic;
using System.Linq;
using Chatbot.Business;
using Chatbot.Commands;

namespace Chatbot.Adapters
{
    public class InMemoryUserConnexions : IUserConnexionCounter, IFollowedUserRetriever, IUserConnexionSaver
    {
        private readonly ISet<UserConnexion> _userConnexions = new HashSet<UserConnexion>();

        public int Count() => _userConnexions.Count;

        public void Save(UserConnexion userConnexion) =>
            _userConnexions.Add(userConnexion);

        public IFollowedUsers RetrieveFollowedUsers(string follower)
        {
            var users = _userConnexions.Where(c => c.Follower == follower).Select(c => c.Followed);
            return new Users(users);
        }

        private class Users : IFollowedUsers, ITimelineUsers
        {
            private readonly IEnumerable<string> _users;

            public Users(IEnumerable<string> users)
            {
                _users = users;
            }

            public ITimelineUsers CombineWith(string user)
            {
                var users = new List<string> { user }.Concat(_users);
                return new Users(users);
            }

            public bool Contains(string user) => _users.Contains(user);
        }

    }
}