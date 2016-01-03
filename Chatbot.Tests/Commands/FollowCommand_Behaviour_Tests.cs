using Chatbot.Commands;
using Chatbot.Control;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class FollowCommand_Behaviour_Tests : IUserConnexionSaver
    {
        private UserConnexion _actualConnexion;
        private State _state;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var followCommand = new FollowCommand(this);
            _state = followCommand.Do("Alice follows Bob");
        }

        [Test]
        public void Saves_user_connexion() => 
            Assert.That(_actualConnexion, Is.EqualTo(new UserConnexion("Alice", "Bob")));

        [Test]
        public void Returns_continue_state() => Assert.That(_state, Is.EqualTo(State.Continue));

        public void Save(UserConnexion userConnexion) =>
            _actualConnexion = userConnexion;
    }
}