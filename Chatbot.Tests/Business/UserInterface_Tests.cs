using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class UserInterface_Tests : IInstructionReader, IInstructionHandler
    {
        private const string ExpectedInstruction = "Expected Instruction";
        private const State ExpectedState = State.Exit;
        private UserInterface _userInterface;
        private State _state;
        private bool _instructionWasRead;
        private string _actualInstruction;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _instructionWasRead = false;
            _actualInstruction = null;
            _userInterface = new UserInterface(this, this);
            _state = _userInterface.ProcessNextInstruction();
        }

        [Test]
        public void Reads_instruction() => Assert.That(_instructionWasRead, Is.True);

        [Test]
        public void Handles_instruction() => Assert.That(_actualInstruction, Is.EqualTo(ExpectedInstruction));

        [Test]
        public void Returns_expected_state() => Assert.That(_state, Is.EqualTo(ExpectedState));

        public string ReadInstruction()
        {
            _instructionWasRead = true;
            return ExpectedInstruction;
        }

        public State HandleInstruction(string instruction)
        {
            _actualInstruction = instruction;
            return ExpectedState;
        }
    }
}