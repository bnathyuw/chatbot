﻿using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class UserInterface_ExitInstruction_Tests : IInstructionReader
    {
        private string _instruction;
        private State _state;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _instruction = "exit";
            var userInterface = new UserInterface(this, new StatusInstructionHandler(null, null, null, null, new ExitInstructionHandler(new UnknownInstructionHandler())));
            _state = userInterface.ProcessNextInstruction();
        }

        [Test]
        public void Returns_an_exit_state() => Assert.That(_state, Is.EqualTo(State.Exit));

        public string ReadInstruction() => _instruction;
    }
}