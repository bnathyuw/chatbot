using System.Collections.Generic;
using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class WallCommandHandler_WithWallCommand_Tests : IFollowedUserRetriever, IMultipleUserMessageRetriever, IMessageDisplayer, IMessageAgeFormatter
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
            var wallCommandHandler = new WallCommandHandler(null, this, this, this, this);
            _actualState = wallCommandHandler.HandleCommand("Daphne wall");
        }

        [Test]
        public void Looks_up_who_that_user_follows() => Assert.That(_actualUser, Is.EqualTo("Daphne"));

        [Test]
        public void Retrieves_messages_for_that_user() => Assert.That(_actualUsers, Does.Contain("Daphne"));

        [TestCase("Emile")]
        [TestCase("Farouk")]
        [TestCase("Gita")]
        public void Retrieves_messages_for_followed_users(string user) => Assert.That(_actualUsers, Does.Contain(user));

        [TestCase("Daphne - Message 1 (age of Message 1)")]
        [TestCase("Emile - Message 2 (age of Message 2)")]
        [TestCase("Emile - Message 3 (age of Message 3)")]
        [TestCase("Farouk - Message 4 (age of Message 4)")]
        [TestCase("Gita - Message 5 (age of Message 5)")]
        public void Displays_the_mssages(string expectedMessage)
            => Assert.That(_actualMessages, Does.Contain(expectedMessage));

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

        public void ShowMessage(string output) => _actualMessages.Add(output);
        public string FormatAge(Message message) => $"age of {message.Text}";
    }
}