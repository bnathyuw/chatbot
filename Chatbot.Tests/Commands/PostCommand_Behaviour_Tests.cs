using System;
using Chatbot.Commands;
using Chatbot.Control;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class PostCommand_Behaviour_Tests : IMessageSaver, ITimestamper
    {
        private const string ExpectedUser = "Alice";
        private const string ExpectedText = "I love the weather today";
        private readonly DateTime _now = new DateTime(2015, 12, 29, 15, 37, 0);
        private Message _messageSaved;
        private State _state;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var postCommand = new PostCommand(this, this);
            _state = postCommand.Do($"{ExpectedUser} -> {ExpectedText}");
        }

        [Test]
        public void Saves_the_message_with_a_timestamp()
        {
            var expectedMessage = new Message { User = ExpectedUser, Text = ExpectedText, SentOn = _now };
            Assert.That(_messageSaved, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Returns_a_continue_state() => Assert.That(_state, Is.EqualTo(State.Continue));

        public void SaveMessage(Message message) => _messageSaved = message;

        public DateTime Timestamp => _now;
    }
}