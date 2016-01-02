using System;
using System.Collections.Generic;
using System.Linq;
using Chatbot.Adapters;
using Chatbot.Commands;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class InMemoryMessages_WallMessage_Tests : IWallMessageDisplayer
    {
        private static readonly DateTime Now = new DateTime(2015, 12, 29, 14, 52, 0);
        private InMemoryMessages _messages;
        private List<Message> _actualMessages;

        private static readonly IEnumerable<Message> ExpectedMessages = new List<Message>
        {
            new Message {User = "Alice", Text = "Message 1", SentOn = Now.AddMinutes(-20)},
            new Message {User = "Bob", Text = "Message 2", SentOn = Now.AddMinutes(-15)},
            new Message {User = "Bob", Text = "Message 3", SentOn = Now.AddMinutes(-12)}
        };

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _messages = new InMemoryMessages();
            foreach (var message in ExpectedMessages)
            {
                _messages.SaveMessage(message);
            }
            _messages.SaveMessage(new Message { User = "Charlie", Text = "Message 4", SentOn = Now.AddMinutes(-10) });
            _actualMessages = new List<Message>();
            var messages = _messages.RetrieveUsersMessages(new List<string> {"Alice", "Bob"});
            messages.Display(this);
        }

        [TestCaseSource(nameof(ExpectedMessages))]
        public void Retrieves_all_messages_for_several_users(Message message) => 
            Assert.That(_actualMessages, Does.Contain(message));

        [Test]
        public void Displays_most_recent_message_first() => 
            Assert.That(_actualMessages.First(), Is.EqualTo(ExpectedMessages.Last()));

        public void DisplayWallMessage(Message message)
        {
            _actualMessages.Add(message);
        }
    }
}