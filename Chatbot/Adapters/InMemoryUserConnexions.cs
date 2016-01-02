﻿using System.Collections.Generic;
using System.Linq;
using Chatbot.Business;
using Chatbot.Commands;

namespace Chatbot.Adapters
{
    public class InMemoryUserConnexions : IUserConnexionCounter, IFollowedUserRetriever, IUserConnexionSaver
    {
        private readonly ISet<UserConnexion> _userConnexions = new HashSet<UserConnexion>();

        public int Count() => _userConnexions.Count;

        public void Save(string follower, string followed) =>
            _userConnexions.Add(new UserConnexion(follower, followed));

        public IFollowedUsers RetrieveFollowedUsers(string follower)
        {
            var users = _userConnexions.Where(c => c.Follower == follower).Select(c => c.Followed);
            return new FollowedUsers(users);
        }

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

        private class FollowedUsers : IFollowedUsers
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

            private class TimelineUsers : ITimelineUsers
            {
                private readonly IEnumerable<string> _users;

                public TimelineUsers(IEnumerable<string> users)
                {
                    _users = users;
                }

                public bool Contains(string user) => _users.Contains(user);
            }

        }

    }
}