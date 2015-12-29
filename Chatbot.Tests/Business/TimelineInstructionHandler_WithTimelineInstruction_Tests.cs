using System;
using System.Collections.Generic;
using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class TimelineInstructionHandler_WithTimelineInstruction_Tests : IMessageDisplayer, IUserMessageRetriever, IClock
    {
        private const string ExpectedUser = "Alice";
        private string _actualMessage;
        private readonly DateTime _now = new DateTime(2015, 12, 28, 22, 30, 0);
        private string _actualUser;
        private State _state;
        private List<string> _actualMessages;

        [SetUp]
        public void SetUp()
        {
            _actualMessage = null;
            _actualMessages = new List<string>();
            _actualUser = null;
            var timelineInstructionHandler = new TimelineInstructionHandler(this, null, this, this);
            _state = timelineInstructionHandler.HandleInstruction(ExpectedUser);
        }

        [Test]
        public void Retrieves_messages_for_the_user_specified() => Assert.That(_actualUser, Is.EqualTo(ExpectedUser));

        [TestCase("Sample message (10 minutes ago)")]
        [TestCase("Another message (1 minute ago)")]
        public void Displays_users_messages(string message) => Assert.That(_actualMessages, Does.Contain(message));

        [Test]
        public void Returns_a_continue_state() =>
            Assert.That(_state, Is.EqualTo(State.Continue));

        public void ShowMessage(string output)
        {
            _actualMessage = output;
            _actualMessages.Add(output);
        }

        public IEnumerable<Message> RetrieveUserMessages(string user)
        {
            _actualUser = user;
            yield return new Message {Text = "Sample message", SentOn = _now.AddMinutes(-10)};
            yield return new Message {Text = "Another message", SentOn = _now.AddMinutes(-1)};
        }

        public DateTime Now => _now;
    }
}