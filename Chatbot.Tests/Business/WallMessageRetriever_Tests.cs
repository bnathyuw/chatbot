using Chatbot.Business;
using Chatbot.Commands;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class WallMessageRetriever_Tests : IFollowedUserRetriever, IMultipleUserMessageRetriever, IFollowedUsers
    {
        private const string ExpectedFollower = "Deirdre";
        private string _actualFollower;
        private ITimelineUsers _actualUsers;
        private IWallMessages _actualMessages;
        private readonly IWallMessages _expectedMessages = new TestWallMessages();
        private readonly ITimelineUsers _expectedUsers = new TestTimelineUsers();
        private string _actualCombinedUser;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _actualFollower = null;
            _actualUsers = null;
            var wallMessageRetriever = new WallMessageRetriever(this, this);

            _actualMessages = wallMessageRetriever.GetWallMessages(ExpectedFollower);
        }

        [Test]
        public void Retrieves_followed_users() => Assert.That(_actualFollower, Is.EqualTo(ExpectedFollower));

        [Test]
        public void Retrieves_messages_for_the_follower() => Assert.That(_actualCombinedUser, Is.EqualTo(ExpectedFollower));

        [Test]
        public void Retrieves_messages_for_followed_user() => Assert.That(_actualUsers, Is.EqualTo(_expectedUsers));

        [Test]
        public void Returns_retrieved_messages() => Assert.That(_actualMessages, Is.EqualTo(_expectedMessages));

        public IFollowedUsers RetrieveFollowedUsers(string follower)
        {
            _actualFollower = follower;
            return this;
        }

        public ITimelineUsers CombineWith(string user)
        {
            _actualCombinedUser = user;
            return _expectedUsers;
        }

        public IWallMessages RetrieveUsersMessages(ITimelineUsers timelineUsers)
        {
            _actualUsers = timelineUsers;
            return _expectedMessages;
        }

        private class TestWallMessages : IWallMessages
        {
            public void Display(IWallMessageDisplayer wallMessageDisplayer)
            {
                throw new System.NotImplementedException();
            }
        }

        private class TestTimelineUsers : ITimelineUsers
        {
            public bool Contains(string user)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}