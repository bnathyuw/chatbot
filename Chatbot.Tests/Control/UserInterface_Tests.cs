using Chatbot.Control;
using NUnit.Framework;

namespace Chatbot.Tests.Control
{
    [TestFixture]
    public class UserInterface_Tests : ICommandReader, ICommandHandler
    {
        private const string ExpectedCommand = "Expected Command";
        private const State ExpectedState = State.Exit;
        private UserInterface _userInterface;
        private State _state;
        private bool _commandWasRead;
        private string _actualCommand;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _commandWasRead = false;
            _actualCommand = null;
            _userInterface = new UserInterface(this, this);
            _state = _userInterface.ProcessNextCommand();
        }

        [Test]
        public void Reads_command() => Assert.That(_commandWasRead, Is.True);

        [Test]
        public void Handles_command() => Assert.That(_actualCommand, Is.EqualTo(ExpectedCommand));

        [Test]
        public void Returns_expected_state() => Assert.That(_state, Is.EqualTo(ExpectedState));

        public string ReadCommand()
        {
            _commandWasRead = true;
            return ExpectedCommand;
        }

        public State Handle(string command)
        {
            _actualCommand = command;
            return ExpectedState;
        }
    }
}