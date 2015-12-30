using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class WallInstructionHandler_WithOtherInstruction_Tests : IInstructionHandler
    {
        private string _actualInstruction;
        private WallInstructionHandler _wallInstructionHandler;
        private const State ExpectedState = State.Exit;

        [SetUp]
        public void SetUp()
        {
            _actualInstruction = null;
            _wallInstructionHandler = new WallInstructionHandler(this, null, null, null, null);
        }

        [TestCase(SampleInstructions.Unknown)]
        [TestCase(SampleInstructions.Exit)]
        [TestCase(SampleInstructions.Status)]
        [TestCase(SampleInstructions.Timeline)]
        public void Passes_instruction_to_successor(string instruction)
        {
            _wallInstructionHandler.HandleInstruction(instruction);
            Assert.That(_actualInstruction, Is.EqualTo(instruction));
        }

        [Test]
        public void Returns_state_from_successor()
        {
            var state = _wallInstructionHandler.HandleInstruction(SampleInstructions.Unknown);
            Assert.That(state, Is.EqualTo(ExpectedState));
        }

        public State HandleInstruction(string instruction)
        {
            _actualInstruction = instruction;
            return ExpectedState;
        }
    }
}