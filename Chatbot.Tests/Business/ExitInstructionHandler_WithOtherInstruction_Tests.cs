using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class ExitInstructionHandler_WithOtherInstruction_Tests : IInstructionHandler
    {
        private string _actualInstruction;
        private ExitInstructionHandler _exitInstructionHandler;
        private const State ExpectedState = State.Exit;

        [SetUp]
        public void SetUp()
        {
            _actualInstruction = null;
            _exitInstructionHandler = new ExitInstructionHandler(this);
        }

        [TestCase(SampleInstructions.Status)]
        [TestCase(SampleInstructions.Unknown)]
        [TestCase(SampleInstructions.Timeline)]
        [TestCase(SampleInstructions.Unknown)]
        [TestCase(SampleInstructions.Follow)]
        [TestCase(SampleInstructions.Wall)]
        public void Passes_instruction_to_successor(string instruction)
        {
            _exitInstructionHandler.HandleInstruction(instruction);
            Assert.That(_actualInstruction, Is.EqualTo(instruction));
        }

        [Test]
        public void Returns_state_from_successor()
        {
            var state = _exitInstructionHandler.HandleInstruction(SampleInstructions.Unknown);
            Assert.That(state, Is.EqualTo(ExpectedState));
        }

        public State HandleInstruction(string instruction)
        {
            _actualInstruction = instruction;
            return ExpectedState;
        }
    }
}