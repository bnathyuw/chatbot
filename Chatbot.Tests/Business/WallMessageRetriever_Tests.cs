using System.Collections.Generic;
using Chatbot.Business;
using Chatbot.Commands;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class WallMessageRetriever_Tests : IFollowedUserRetriever, IMultipleUserMessageRetriever
    {
        private const string ExpectedFollower = "Deirdre";
        private string _actualFollower;
        private IEnumerable<string> _actualUsers;
        private IWallMessages _expectedMessages;
        private IWallMessages _actualMessages;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _actualFollower = null;
            _actualUsers = new List<string>();
            _expectedMessages = new TestWallMessages();
            var wallMessageRetriever = new WallMessageRetriever(this, this);

            _actualMessages = wallMessageRetriever.GetWallMessages(ExpectedFollower);
        }

        [Test]
        public void Retrieves_followed_users() => Assert.That(_actualFollower, Is.EqualTo(ExpectedFollower));

        [Test]
        public void Retrieves_messages_for_the_follower() => Assert.That(_actualUsers, Does.Contain(ExpectedFollower));

        [TestCase("Engelbert")]
        [TestCase("Fionnuala")]
        public void Retrieves_messages_for_followed_user(string expectedUser) =>
            Assert.That(_actualUsers, Does.Contain(expectedUser));

        [Test]
        public void Returns_retrieved_messages() => Assert.That(_actualMessages, Is.EqualTo(_expectedMessages));

        public IEnumerable<string> RetrieveFollowedUsers(string follower)
        {
            _actualFollower = follower;
            return new[] {"Engelbert", "Fionnuala"};
        }

        public IWallMessages RetrieveUsersMessages(IEnumerable<string> users)
        {
            _actualUsers = users;
            return _expectedMessages;
        }

        private class TestWallMessages : IWallMessages
        {
            public void Display(IWallMessageDisplayer wallMessageDisplayer)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}