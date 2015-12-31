using System.Linq;
using Chatbot.Adapters;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class UserConnexionStore_WithConnexions_Tests
    {
        private UserConnexionStore _userConnexionStore;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _userConnexionStore = new UserConnexionStore();

            _userConnexionStore.SaveConnexion("Alice", "Bob");
            _userConnexionStore.SaveConnexion("Alice", "Charlie");
            _userConnexionStore.SaveConnexion("Charlie", "Bob");
        }

        [Test]
        public void Counts_user_connexions() => Assert.That(_userConnexionStore.CountUserConnexions(), Is.EqualTo(3));

        [TestCase("Alice", 2)]
        [TestCase("Charlie", 1)]
        public void Retrieves_all_connections_for_a_single_user(string user, int expectedConnexionCount) =>
            Assert.That(_userConnexionStore.RetrieveFollowedUsers(user).Count(), Is.EqualTo(expectedConnexionCount));

        [TestCase("Alice", "Bob")]
        [TestCase("Alice", "Charlie")]
        [TestCase("Charlie", "Bob")]
        public void Retrieves_correct_followed_users(string follower, string followed) =>
            Assert.That(_userConnexionStore.RetrieveFollowedUsers(follower), Does.Contain(followed));
    }
}