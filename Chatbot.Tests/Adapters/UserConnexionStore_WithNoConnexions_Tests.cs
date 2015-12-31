using Chatbot.Adapters;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class UserConnexionStore_WithNoConnexions_Tests
    {
        private UserConnexionStore _userConnexionStore;

        [OneTimeSetUp]
        public void OneTimeSetUp() => _userConnexionStore = new UserConnexionStore();

        [Test]
        public void Counts_zero_messages() => Assert.That(_userConnexionStore.CountUserConnexions(), Is.EqualTo(0));
    }
}