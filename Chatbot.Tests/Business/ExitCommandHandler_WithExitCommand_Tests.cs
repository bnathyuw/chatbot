using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class ExitCommandHandler_WithExitCommand_Tests
    {
        private State _state;

        [SetUp]
        public void SetUp()
        {
            var exitCommandHandler = new ExitCommandHandler(null);
            _state = exitCommandHandler.HandleCommand(SampleCommands.Exit);
        }

        [Test]
        public void Returns_an_exit_state() => Assert.That(_state, Is.EqualTo(State.Exit));
    }
}