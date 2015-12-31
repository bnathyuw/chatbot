using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class ExitCommandHandler_WithExitCommand_Tests
    {
        private State _state;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var exitCommandHandler = new ExitCommandHandler(null);
            _state = exitCommandHandler.Handle(SampleCommands.Exit);
        }

        [Test]
        public void Returns_an_exit_state() => Assert.That(_state, Is.EqualTo(State.Exit));
    }
}