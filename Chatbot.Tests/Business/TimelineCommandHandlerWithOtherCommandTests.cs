using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class TimelineCommandHandlerWithOtherCommandTests : ICommandHandler
    {
        private string _actualCommand;
        private TimelineCommandHandler _timelineCommandHandler;
        private const State ExpectedState = State.Exit;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _actualCommand = null;
            _timelineCommandHandler = new TimelineCommandHandler(this, null, null, null);
        }

        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Exit)]
        [TestCase(SampleCommands.Status)]
        [TestCase(SampleCommands.Post)]
        [TestCase(SampleCommands.Follow)]
        [TestCase(SampleCommands.Wall)]
        public void Passes_command_to_successor(string command)
        {
            _timelineCommandHandler.HandleCommand(command);
            Assert.That(_actualCommand, Is.EqualTo(command));
        }

        [Test]
        public void Returns_state_from_successor()
        {
            var state = _timelineCommandHandler.HandleCommand(SampleCommands.Unknown);
            Assert.That(state, Is.EqualTo(ExpectedState));
        }

        public State HandleCommand(string command)
        {
            _actualCommand = command;
            return ExpectedState;
        }
    }
}