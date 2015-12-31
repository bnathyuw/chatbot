using System;
using System.Collections.Generic;
using System.Linq;
using Chatbot.Adapters;
using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class MessageStore_WithMessages_Tests
    {
        private readonly DateTime _now = new DateTime(2015, 12, 29, 14, 52, 0);
        private InMemoryMessages _Messages;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _Messages = new InMemoryMessages();
            _Messages.SaveMessage(new Message { User = "Alice", Text = "Message 1", SentOn = _now.AddMinutes(-20) });
            _Messages.SaveMessage(new Message { User = "Bob", Text = "Message 2", SentOn = _now.AddMinutes(-15) });
            _Messages.SaveMessage(new Message { User = "Bob", Text = "Message 3", SentOn = _now.AddMinutes(-12) });
        }

        [Test]
        public void Counts_the_messages() => Assert.That(_Messages.Count(), Is.EqualTo(3));

        [TestCase("Alice", 1)]
        [TestCase("Bob", 2)]
        public void Retrieves_all_messages_for_a_single_user(string user, int expectedMessageCount) =>
            Assert.That(_Messages.RetrieveUserMessages(user).Count(), Is.EqualTo(expectedMessageCount));

        [Test]
        public void Retrieves_most_recent_message_first_for_a_single_user() =>
            Assert.That(_Messages.RetrieveUserMessages("Bob").First().Text, Is.EqualTo("Message 3"));

        [TestCase("Alice", 1)]
        [TestCase("Bob", 2)]
        public void Retrieves_all_messages_for_one_user_in_a_list(string user, int expectedMessageCount) =>
            Assert.That(_Messages.RetrieveUsersMessages(new List<string> {user}).Count(), Is.EqualTo(expectedMessageCount));

        [Test]
        public void Retrieves_most_recent_message_first_for_one_user_in_a_list() =>
                Assert.That(_Messages.RetrieveUsersMessages(new List<string> {"Bob"}).First().Text, Is.EqualTo("Message 3"));

        [Test]
        public void Retrieves_all_messages_for_several_users() =>
            Assert.That(_Messages.RetrieveUsersMessages(new List<string> {"Alice", "Bob"}).Count, Is.EqualTo(3));
    }
}