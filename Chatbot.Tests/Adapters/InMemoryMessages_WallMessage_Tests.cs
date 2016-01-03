using System;
using System.Collections.Generic;
using System.Linq;
using Chatbot.Adapters;
using Chatbot.Business;
using Chatbot.Commands;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class InMemoryMessages_WallMessage_Tests : IWallMessageDisplayer, ITimelineUsers
    {
        private static readonly DateTime Now = new DateTime(2015, 12, 29, 14, 52, 0);
        private List<Message> _actualMessages;

        private static readonly IEnumerable<Message> ExpectedMessages = new List<Message>
        {
            new Message("Alice", "Message 1", Now.AddMinutes(-20)),
            new Message("Bob", "Message 2", Now.AddMinutes(-15)),
            new Message("Bob", "Message 3", Now.AddMinutes(-12))
        };

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var inMemoryMessages = new InMemoryMessages();
            foreach (var message in ExpectedMessages)
            {
                inMemoryMessages.SaveMessage(message);
            }
            inMemoryMessages.SaveMessage(new Message("Charlie", "Message 4", Now.AddMinutes(-10)));
            _actualMessages = new List<Message>();

            var messages = inMemoryMessages.RetrieveUsersMessages(this);
            messages.Display(this);
        }

        [TestCaseSource(nameof(ExpectedMessages))]
        public void Retrieves_all_messages_for_several_users(Message message) =>
            Assert.That(_actualMessages, Does.Contain(message));

        [Test]
        public void Displays_most_recent_message_first() =>
            Assert.That(_actualMessages.First(), Is.EqualTo(ExpectedMessages.Last()));

        public void DisplayWallMessage(Message message) => _actualMessages.Add(message);

        public bool Contains(string user) => user == "Alice" || user == "Bob";
    }
}