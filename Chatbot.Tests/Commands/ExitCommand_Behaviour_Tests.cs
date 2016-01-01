using Chatbot.Commands;
using Chatbot.Control;
using Chatbot.Tests.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class ExitCommand_Behaviour_Tests
    {
        private State _state;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var exitCommand = new ExitCommand();
            _state = exitCommand.Do(SampleCommands.Exit);
        }

        [Test]
        public void Returns_an_exit_state() => Assert.That(_state, Is.EqualTo(State.Exit));
    }
}