using System;
using Chatbot.Adapters;
using Chatbot.Commands;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class InMemoryMessages_Tests
    {
        private readonly DateTime _now = new DateTime(2015, 12, 29, 14, 52, 0);
        private InMemoryMessages _messages;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _messages = new InMemoryMessages();
            _messages.SaveMessage(new Message("Alice", "Message 1", _now.AddMinutes(-20)));
            _messages.SaveMessage(new Message("Bob", "Message 2", _now.AddMinutes(-15)));
            _messages.SaveMessage(new Message("Bob", "Message 3", _now.AddMinutes(-12)));
        }

        [Test]
        public void Counts_the_messages() => Assert.That(_messages.Count(), Is.EqualTo(3));
    }
}