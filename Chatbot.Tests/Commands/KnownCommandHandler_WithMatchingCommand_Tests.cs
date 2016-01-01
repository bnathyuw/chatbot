using Chatbot.Commands;
using Chatbot.Control;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class KnownCommandHandler_WithMatchingCommand_Tests : ICommand
    {
        private State _state;
        private const State ExpectedState = State.Continue;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var commandHandler = new KnownCommandHandler(null, this);
            _state = commandHandler.Handle("Command");
        }

        [Test]
        public void Returns_state_from_command() => Assert.That(_state, Is.EqualTo(ExpectedState));

        public bool Matches(string command) => true;

        public State Do(string command) => ExpectedState;
    }
}