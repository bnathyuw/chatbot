using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class FollowInstructionHandler_WithOtherInstruction_Tests : IInstructionHandler
    {
        private string _actualInstruction;
        private FollowInstructionHandler _followInstructionHandler;
        private const State ExpectedState = State.Exit;

        [SetUp]
        public void SetUp()
        {
            _actualInstruction = null;
            _followInstructionHandler = new FollowInstructionHandler(this, null);
        }

        [TestCase(SampleInstructions.Status)]
        [TestCase(SampleInstructions.Unknown)]
        [TestCase(SampleInstructions.Timeline)]
        [TestCase(SampleInstructions.Unknown)]
        [TestCase(SampleInstructions.Exit)]
        [TestCase(SampleInstructions.Wall)]
        public void Passes_instruction_to_successor(string instruction)
        {
            _followInstructionHandler.HandleInstruction(instruction);
            Assert.That(_actualInstruction, Is.EqualTo(instruction));
        }

        [Test]
        public void Returns_state_from_successor()
        {
            var state = _followInstructionHandler.HandleInstruction(SampleInstructions.Unknown);
            Assert.That(state, Is.EqualTo(ExpectedState));
        }

        public State HandleInstruction(string instruction)
        {
            _actualInstruction = instruction;
            return ExpectedState;
        }

    }
}