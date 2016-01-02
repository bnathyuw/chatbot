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
    }
}