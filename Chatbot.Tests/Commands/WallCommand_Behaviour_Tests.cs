using System.Collections.Generic;
using Chatbot.Commands;
using Chatbot.Control;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class WallCommand_Behaviour_Tests : IFollowedUserRetriever, IMultipleUserMessageRetriever, IWallMessageDisplayer
    {
        private readonly IEnumerable<string> _expectedFollowList = new List<string> {"Emile", "Farouk", "Gita"};
        private readonly IEnumerable<Message> _expectedMessages = new List<Message>
        {
            new Message {User = "Daphne", Text = "Message 1"},
            new Message {User = "Emile", Text = "Message 2"},
            new Message {User = "Emile", Text = "Message 3"},
            new Message {User = "Farouk", Text = "Message 4"},
            new Message {User = "Gita", Text = "Message 5"}
        };

        private string _actualUser;
        private IEnumerable<string> _actualUsers;
        private IList<string> _actualMessages;
        private State _actualState;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _actualUser = null;
            _actualUsers = new List<string>();
            _actualMessages = new List<string>();
            var wallCommand = new WallCommand(this, this, this);
            _actualState = wallCommand.Do("Daphne wall");
        }

        [Test]
        public void Looks_up_who_that_user_follows() => Assert.That(_actualUser, Is.EqualTo("Daphne"));

        [Test]
        public void Retrieves_messages_for_that_user() => Assert.That(_actualUsers, Does.Contain("Daphne"));

        [TestCase("Emile")]
        [TestCase("Farouk")]
        [TestCase("Gita")]
        public void Retrieves_messages_for_followed_users(string user) => Assert.That(_actualUsers, Does.Contain(user));

        [TestCase("Message 1")]
        [TestCase("Message 2")]
        [TestCase("Message 3")]
        [TestCase("Message 4")]
        [TestCase("Message 5")]
        public void Displays_the_messages(string expectedMessage) =>
            Assert.That(_actualMessages, Does.Contain(expectedMessage));

        [Test]
        public void Returns_continue_state() => Assert.That(_actualState, Is.EqualTo(State.Continue));

        public IEnumerable<string> RetrieveFollowedUsers(string user)
        {
            _actualUser = user;
            return _expectedFollowList;
        }

        public IEnumerable<Message> RetrieveUsersMessages(IEnumerable<string> users)
        {
            _actualUsers = users;
            return _expectedMessages;
        }

        public void DisplayWallMessage(Message message) => _actualMessages.Add(message.Text);
    }
}