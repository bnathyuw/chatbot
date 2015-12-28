﻿using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class StatusInstructionHandler_WithOtherInstruction_Tests : IInstructionHandler
    {
        private string _actualInstruction;
        private StatusInstructionHandler _statusInstructionHandler;
        private const State ExpectedState = State.Exit;

        [SetUp]
        public void SetUp()
        {
            _actualInstruction = null;
            _statusInstructionHandler = new StatusInstructionHandler(null, null, null, null, this);
        }

        [TestCase("exit")]
        [TestCase("Unknown instruction")]
        public void Passes_instruction_to_successor(string instruction)
        {
            _statusInstructionHandler.HandleInstruction(instruction);
            Assert.That(_actualInstruction, Is.EqualTo(instruction));
        }

        [Test]
        public void Returns_state_from_successor()
        {
            var state = _statusInstructionHandler.HandleInstruction("Unknown instruction");
            Assert.That(state, Is.EqualTo(ExpectedState));
        }

        public State HandleInstruction(string instruction)
        {
            _actualInstruction = instruction;
            return ExpectedState;
        }
    }
}