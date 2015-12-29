using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class PostInstructionHandler_WithOtherInstruction_Tests : IInstructionHandler
    {
        private string _actualInstruction;
        private PostInstructionHandler _postInstructionHandler;
        private const State ExpectedState = State.Exit;

        [SetUp]
        public void SetUp()
        {
            _actualInstruction = null;
            _postInstructionHandler = new PostInstructionHandler(null, null, this);
        }

        [TestCase(SampleInstructions.Unknown)]
        [TestCase(SampleInstructions.Exit)]
        [TestCase(SampleInstructions.Status)]
        [TestCase(SampleInstructions.Timeline)]
        public void Passes_instruction_to_successor(string instruction)
        {
            _postInstructionHandler.HandleInstruction(instruction);
            Assert.That(_actualInstruction, Is.EqualTo(instruction));
        }

        [Test]
        public void Returns_state_from_successor()
        {
            var state = _postInstructionHandler.HandleInstruction(SampleInstructions.Unknown);
            Assert.That(state, Is.EqualTo(ExpectedState));
        }

        public State HandleInstruction(string instruction)
        {
            _actualInstruction = instruction;
            return ExpectedState;
        }
    }
}