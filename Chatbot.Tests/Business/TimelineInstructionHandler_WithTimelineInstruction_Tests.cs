using System;
using System.Collections.Generic;
using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class TimelineInstructionHandler_WithTimelineInstruction_Tests : IMessageDisplayer, IUserMessageRetriever, IClock
    {
        private string _actualMessage;
        private readonly DateTime _now = new DateTime(2015, 12, 28, 22, 30, 0);
        private string _actualUser;
        private State _state;

        [SetUp]
        public void SetUp()
        {
            _actualMessage = null;
            _actualUser = null;
            var timelineInstructionHandler = new TimelineInstructionHandler(this, null, this, this);
            _state = timelineInstructionHandler.HandleInstruction("Alice");
        }

        [Test]
        public void Retrieves_messages_for_the_user_specified() => Assert.That(_actualUser, Is.EqualTo("Alice"));

        [Test]
        public void Displays_users_messages() => 
            Assert.That(_actualMessage, Is.EqualTo("Sample message (10 minutes ago)"));

        [Test]
        public void Returns_a_continue_state() =>
            Assert.That(_state, Is.EqualTo(State.Continue));

        public void ShowMessage(string output)
        {
            _actualMessage = output;
        }

        public IEnumerable<Message> RetrieveUserMessages(string user)
        {
            _actualUser = user;
            yield return new Message {Text = "Sample message", SentOn = _now.AddMinutes(-10)};
        }

        public DateTime Now => _now;
    }
}