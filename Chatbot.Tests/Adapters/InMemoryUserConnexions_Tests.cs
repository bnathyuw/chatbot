﻿using Chatbot.Adapters;
using Chatbot.Commands;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class InMemoryUserConnexions_Tests
    {
        private InMemoryUserConnexions _userConnexions;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _userConnexions = new InMemoryUserConnexions();

            _userConnexions.Save(new UserConnexion("Alice", "Bob"));
            _userConnexions.Save(new UserConnexion("Alice", "Charlie"));
            _userConnexions.Save(new UserConnexion("Charlie", "Bob"));
        }

        [Test]
        public void Counts_user_connexions() => Assert.That(_userConnexions.Count(), Is.EqualTo(3));

        [TestCase("Alice", "Bob")]
        [TestCase("Alice", "Charlie")]
        [TestCase("Charlie", "Bob")]
        public void Retrieves_correct_followed_users(string follower, string followed) =>
            Assert.That(_userConnexions.RetrieveFollowedUsers(follower).CombineWith("dummy").Contains(followed), Is.True);
    }
}