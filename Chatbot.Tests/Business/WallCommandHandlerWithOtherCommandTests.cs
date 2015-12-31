using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class WallCommandHandlerWithOtherCommandTests : ICommandHandler
    {
        private string _actualCommand;
        private WallCommandHandler _wallCommandHandler;
        private const State ExpectedState = State.Exit;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _actualCommand = null;
            _wallCommandHandler = new WallCommandHandler(this, null, null, null, null);
        }

        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Exit)]
        [TestCase(SampleCommands.Status)]
        [TestCase(SampleCommands.Timeline)]
        [TestCase(SampleCommands.Follow)]
        [TestCase(SampleCommands.Post)]
        public void Passes_command_to_successor(string command)
        {
            _wallCommandHandler.Handle(command);
            Assert.That(_actualCommand, Is.EqualTo(command));
        }

        [Test]
        public void Returns_state_from_successor()
        {
            var state = _wallCommandHandler.Handle(SampleCommands.Unknown);
            Assert.That(state, Is.EqualTo(ExpectedState));
        }

        public State Handle(string command)
        {
            _actualCommand = command;
            return ExpectedState;
        }
    }
}