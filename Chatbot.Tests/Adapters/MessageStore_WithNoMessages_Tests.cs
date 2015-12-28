using Chatbot.Adapters;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class MessageStore_WithNoMessages_Tests
    {
        private MessageStore _messageStore;

        [SetUp]
        public void SetUp() => _messageStore = new MessageStore(null);

        [Test]
        public void Counts_zero_messages() => Assert.That(_messageStore.CountMessages(), Is.EqualTo(0));
    }
}