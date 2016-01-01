using System.Collections.Generic;
using Chatbot.Commands;
using Chatbot.Control;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class TimelineCommand_Behaviour_Tests : IUserMessageRetriever, ITimelineMessageDisplayer
    {
        private const string ExpectedUser = "Alice";
        private string _actualUser;
        private State _state;
        private List<string> _actualMessages;
        private readonly Message _message1 = new Message {Text = "Sample message"};
        private readonly Message _message2 = new Message {Text = "Another message"};

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _actualMessages = new List<string>();
            _actualUser = null;
            var timelineCommand = new TimelineCommand(this, this);
            _state = timelineCommand.Do(ExpectedUser);
        }

        [Test]
        public void Retrieves_messages_for_the_user_specified() => Assert.That(_actualUser, Is.EqualTo(ExpectedUser));

        [TestCase("Sample message")]
        [TestCase("Another message")]
        public void Displays_user_messages(string expectedMessage) => 
            Assert.That(_actualMessages, Does.Contain(expectedMessage));

        [Test]
        public void Returns_a_continue_state() =>
            Assert.That(_state, Is.EqualTo(State.Continue));

        public IEnumerable<Message> RetrieveUserMessages(string user)
        {
            _actualUser = user;
            yield return _message1;
            yield return _message2;
        }

        public void DisplayTimelineMessage(Message message) => _actualMessages.Add(message.Text);
    }
}