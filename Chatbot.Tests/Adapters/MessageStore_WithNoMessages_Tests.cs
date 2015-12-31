﻿using Chatbot.Adapters;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class MessageStore_WithNoMessages_Tests
    {
        private MessageStore _messageStore;

        [OneTimeSetUp]
        public void OneTimeSetUp() => _messageStore = new MessageStore();

        [Test]
        public void Counts_zero_messages() => Assert.That(_messageStore.CountMessages(), Is.EqualTo(0));
    }
}