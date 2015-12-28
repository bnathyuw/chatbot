using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class ExitInstructionHandler_WithExitInstruction_Tests
    {
        private State _state;

        [SetUp]
        public void SetUp()
        {
            var exitInstructionHandler = new ExitInstructionHandler(null);
            _state = exitInstructionHandler.HandleInstruction("exit");
        }

        [Test]
        public void Returns_an_exit_state() => Assert.That(_state, Is.EqualTo(State.Exit));
    }
}