using System;
using System.Linq;
using Chatbot.Adapters;
using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class MessageStore_WithThreeMessages_Tests : IClock
    {
        private readonly DateTime _now = new DateTime(2015, 12, 29, 14, 52, 0);
        private MessageStore _messageStore;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _messageStore = new MessageStore();
            _messageStore.SaveMessage(new Message { User = "Alice", Text = "Message 1", SentOn = _now.AddMinutes(-20) });
            _messageStore.SaveMessage(new Message { User = "Bob", Text = "Message 2", SentOn = _now.AddMinutes(-15) });
            _messageStore.SaveMessage(new Message { User = "Bob", Text = "Message 3", SentOn = _now.AddMinutes(-12) });
        }

        [Test]
        public void Counts_three_messages() => Assert.That(_messageStore.CountMessages(), Is.EqualTo(3));

        [TestCase("Alice", 1)]
        [TestCase("Bob", 2)]
        public void Retrieves_all_a_users_messages(string user, int expectedMessageCount) =>
            Assert.That(_messageStore.RetrieveUserMessages(user).Count(), Is.EqualTo(expectedMessageCount));

        [Test]
        public void Retrieves_most_recent_message_first() =>
            Assert.That(_messageStore.RetrieveUserMessages("Bob").First().Text, Is.EqualTo("Message 3"));

        public DateTime Now => _now;
    }
}