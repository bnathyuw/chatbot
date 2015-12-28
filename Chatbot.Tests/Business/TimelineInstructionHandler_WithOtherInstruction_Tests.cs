using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class TimelineInstructionHandler_WithOtherInstruction_Tests : IInstructionHandler
    {
        private string _actualInstruction;
        private TimelineInstructionHandler _timelineInstructionHandler;
        private const State ExpectedState = State.Exit;

        [SetUp]
        public void SetUp()
        {
            _actualInstruction = null;
            _timelineInstructionHandler = new TimelineInstructionHandler(null, this, null, null);
        }

        [TestCase("Unknown instruction")]
        [TestCase("exit")]
        [TestCase("status")]
        public void Passes_instruction_to_successor(string instruction)
        {
            _timelineInstructionHandler.HandleInstruction(instruction);
            Assert.That(_actualInstruction, Is.EqualTo(instruction));
        }

        [Test]
        public void Returns_state_from_successor()
        {
            var state = _timelineInstructionHandler.HandleInstruction("Unknown instruction");
            Assert.That(state, Is.EqualTo(ExpectedState));
        }

        public State HandleInstruction(string instruction)
        {
            _actualInstruction = instruction;
            return ExpectedState;
        }
    }
}