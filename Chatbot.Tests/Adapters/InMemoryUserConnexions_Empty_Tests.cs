using Chatbot.Adapters;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class InMemoryUserConnexions_Empty_Tests
    {
        private InMemoryUserConnexions _userConnexions;

        [OneTimeSetUp]
        public void OneTimeSetUp() => _userConnexions = new InMemoryUserConnexions();

        [Test]
        public void Counts_zero_messages() => Assert.That(_userConnexions.Count(), Is.EqualTo(0));
    }
}