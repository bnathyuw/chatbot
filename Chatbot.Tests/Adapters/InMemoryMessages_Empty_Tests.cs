using Chatbot.Adapters;
using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class InMemoryMessages_Empty_Tests
    {
        private IMessageCounter _messages;

        [OneTimeSetUp]
        public void OneTimeSetUp() => _messages = new InMemoryMessages();

        [Test]
        public void Counts_zero_messages() => Assert.That(_messages.Count(), Is.EqualTo(0));
    }
}