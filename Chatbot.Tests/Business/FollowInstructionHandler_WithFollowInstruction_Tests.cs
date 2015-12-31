using System;
using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class FollowInstructionHandler_WithFollowInstruction_Tests : IUserConnexionSaver
    {
        private Tuple<string, string> _actualConnexion;
        private State _state;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var followInstructionHandler = new FollowInstructionHandler(null, this);
            _state = followInstructionHandler.HandleInstruction("Alice follows Bob");
        }

        [Test]
        public void Saves_user_connexion() => 
            Assert.That(_actualConnexion, Is.EqualTo(new Tuple<string, string>("Alice", "Bob")));

        [Test]
        public void Returns_continue_state() => Assert.That(_state, Is.EqualTo(State.Continue));

        public void SaveConnexion(string follower, string followed) =>
            _actualConnexion = new Tuple<string, string>(follower, followed);
    }
}