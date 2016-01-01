using System;
using Chatbot.Commands;
using Chatbot.Control;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class FollowCommandHandler_WithFollowCommand_Tests : IUserConnexionSaver
    {
        private Tuple<string, string> _actualConnexion;
        private State _state;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var followCommandHandler = new FollowCommandHandler(null, this);
            _state = followCommandHandler.Handle("Alice follows Bob");
        }

        [Test]
        public void Saves_user_connexion() => 
            Assert.That(_actualConnexion, Is.EqualTo(new Tuple<string, string>("Alice", "Bob")));

        [Test]
        public void Returns_continue_state() => Assert.That(_state, Is.EqualTo(State.Continue));

        public void Save(string follower, string followed) =>
            _actualConnexion = new Tuple<string, string>(follower, followed);
    }
}