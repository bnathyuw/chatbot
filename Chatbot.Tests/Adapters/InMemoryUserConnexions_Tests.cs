using System.Linq;
using Chatbot.Adapters;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class InMemoryUserConnexions_Tests
    {
        private InMemoryUserConnexions _userConnexions;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _userConnexions = new InMemoryUserConnexions();

            _userConnexions.Save("Alice", "Bob");
            _userConnexions.Save("Alice", "Charlie");
            _userConnexions.Save("Charlie", "Bob");
        }

        [Test]
        public void Counts_user_connexions() => Assert.That(_userConnexions.Count(), Is.EqualTo(3));

        [TestCase("Alice", 2)]
        [TestCase("Charlie", 1)]
        public void Retrieves_all_connections_for_a_single_user(string user, int expectedConnexionCount) =>
            Assert.That(_userConnexions.RetrieveFollowedUsers(user).Count(), Is.EqualTo(expectedConnexionCount));

        [TestCase("Alice", "Bob")]
        [TestCase("Alice", "Charlie")]
        [TestCase("Charlie", "Bob")]
        public void Retrieves_correct_followed_users(string follower, string followed) =>
            Assert.That(_userConnexions.RetrieveFollowedUsers(follower), Does.Contain(followed));
    }
}