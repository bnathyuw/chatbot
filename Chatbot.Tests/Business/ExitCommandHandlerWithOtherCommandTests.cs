using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class ExitCommandHandlerWithOtherCommandTests : ICommandHandler
    {
        private string _actualCommand;
        private ExitCommandHandler _exitCommandHandler;
        private const State ExpectedState = State.Exit;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _actualCommand = null;
            _exitCommandHandler = new ExitCommandHandler(this);
        }

        [TestCase(SampleCommands.Status)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Timeline)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Follow)]
        [TestCase(SampleCommands.Wall)]
        public void Passes_command_to_successor(string command)
        {
            _exitCommandHandler.Handle(command);
            Assert.That(_actualCommand, Is.EqualTo(command));
        }

        [Test]
        public void Returns_state_from_successor()
        {
            var state = _exitCommandHandler.Handle(SampleCommands.Unknown);
            Assert.That(state, Is.EqualTo(ExpectedState));
        }

        public State Handle(string command)
        {
            _actualCommand = command;
            return ExpectedState;
        }
    }
}