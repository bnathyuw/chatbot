using Chatbot.Adapters;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class MessageStore_WithNoMessages_Tests
    {
        private InMemoryMessages _Messages;

        [OneTimeSetUp]
        public void OneTimeSetUp() => _Messages = new InMemoryMessages();

        [Test]
        public void Counts_zero_messages() => Assert.That(_Messages.Count(), Is.EqualTo(0));
    }
}