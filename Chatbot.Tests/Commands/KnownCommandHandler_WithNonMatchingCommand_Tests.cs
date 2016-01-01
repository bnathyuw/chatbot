using Chatbot.Commands;
using Chatbot.Control;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class KnownCommandHandler_WithNonMatchingCommand_Tests : ICommand, ICommandHandler
    {
        private const State ExpectedState = State.Continue;
        private State _state;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var commandHandler = new KnownCommandHandler(this, this);
            _state = commandHandler.Handle("Command");
        }

        [Test]
        public void Returns_state_from_successor() => Assert.That(_state, Is.EqualTo(ExpectedState));

        public bool Matches(string command) => false;

        public State Do(string command)
        {
            throw new System.NotImplementedException();
        }

        public State Handle(string command) => ExpectedState;
    }
}