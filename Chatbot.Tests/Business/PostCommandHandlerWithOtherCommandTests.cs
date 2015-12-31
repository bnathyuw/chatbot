using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class PostCommandHandlerWithOtherCommandTests : ICommandHandler
    {
        private string _actualCommand;
        private PostCommandHandler _postCommandHandler;
        private const State ExpectedState = State.Exit;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _actualCommand = null;
            _postCommandHandler = new PostCommandHandler(null, null, this);
        }

        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Exit)]
        [TestCase(SampleCommands.Status)]
        [TestCase(SampleCommands.Timeline)]
        [TestCase(SampleCommands.Follow)]
        [TestCase(SampleCommands.Wall)]
        public void Passes_command_to_successor(string command)
        {
            _postCommandHandler.HandleCommand(command);
            Assert.That(_actualCommand, Is.EqualTo(command));
        }

        [Test]
        public void Returns_state_from_successor()
        {
            var state = _postCommandHandler.HandleCommand(SampleCommands.Unknown);
            Assert.That(state, Is.EqualTo(ExpectedState));
        }

        public State HandleCommand(string command)
        {
            _actualCommand = command;
            return ExpectedState;
        }
    }
}