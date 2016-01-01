using Chatbot.Commands;
using Chatbot.Control;
using Chatbot.Tests.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class FollowCommandHandlerWithOtherCommandTests : ICommandHandler
    {
        private string _actualCommand;
        private FollowCommandHandler _followCommandHandler;
        private const State ExpectedState = State.Exit;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _actualCommand = null;
            _followCommandHandler = new FollowCommandHandler(this, null);
        }

        [TestCase(SampleCommands.Status)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Timeline)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Exit)]
        [TestCase(SampleCommands.Wall)]
        public void Passes_command_to_successor(string command)
        {
            _followCommandHandler.Handle(command);
            Assert.That(_actualCommand, Is.EqualTo(command));
        }

        [Test]
        public void Returns_state_from_successor()
        {
            var state = _followCommandHandler.Handle(SampleCommands.Unknown);
            Assert.That(state, Is.EqualTo(ExpectedState));
        }

        public State Handle(string command)
        {
            _actualCommand = command;
            return ExpectedState;
        }

    }
}