using System;
using System.Collections.Generic;
using System.Linq;
using Chatbot.Adapters;
using Chatbot.Commands;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class InMemoryMessages_TimelineMessages_Tests : ITimelineMessageDisplayer
    {
        private static readonly DateTime Now = new DateTime(2015, 12, 29, 14, 52, 0);
        private List<Message> _actualMessages;

        private static readonly IEnumerable<Message> ExpectedMessages = new List<Message>
        {
            new Message {User = "Bob", Text = "Message 2", SentOn = Now.AddMinutes(-15)},
            new Message {User = "Bob", Text = "Message 3", SentOn = Now.AddMinutes(-12)}
        };

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _actualMessages = new List<Message>();
            var inMemoryMessages = new InMemoryMessages();
            inMemoryMessages.SaveMessage(new Message {User = "Alice", Text = "Message 1", SentOn = Now.AddMinutes(-20)});
            foreach (var message in ExpectedMessages)
            {
                inMemoryMessages.SaveMessage(message);
            }
            var messages = inMemoryMessages.RetrieveUserMessages("Bob");
            messages.Display(this);
        }

        [TestCaseSource(nameof(ExpectedMessages))]
        public void Retrieves_all_messages_for_specified_user(Message message) =>
            Assert.That(_actualMessages, Does.Contain(message));

        [Test]
        public void Displays_most_recent_message_first() =>
            Assert.That(_actualMessages.First(), Is.EqualTo(ExpectedMessages.Last()));

        public void DisplayTimelineMessage(Message message) => _actualMessages.Add(message);
    }
}