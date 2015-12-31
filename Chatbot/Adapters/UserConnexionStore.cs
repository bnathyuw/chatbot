using System.Collections.Generic;
using System.Linq;
using Chatbot.Business;

namespace Chatbot.Adapters
{
    public class UserConnexionStore : IUserConnexionCounter, IFollowedUserRetriever
    {
        private readonly ISet<UserConnexion> _userConnexions = new HashSet<UserConnexion>();

        public int CountUserConnexions() => _userConnexions.Count;

        public void SaveConnexion(string follower, string followed) =>
            _userConnexions.Add(new UserConnexion(follower, followed));

        public IEnumerable<string> RetrieveFollowedUsers(string user) =>
            _userConnexions.Where(c => c.Follower == user).Select(c => c.Followed);

        private struct UserConnexion
        {
            public string Follower { get; }
            public string Followed { get; }

            public UserConnexion(string follower, string followed)
            {
                Follower = follower;
                Followed = followed;
            }
        }
    }
}