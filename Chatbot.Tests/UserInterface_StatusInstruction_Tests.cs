using System;
using System.Collections.Generic;
using Chatbot.Business;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Chatbot.Tests
{
    [TestFixture]
    public class UserInterface_StatusInstruction_Tests : IInstructionReader, IMessageDisplayer, IClock, IMessageCounter, IUserConnexionCounter
    {
        private string _instruction;
        private IList<string> _actualMessages;
        private UserInterface _userInterface;
        private State _state;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _instruction = "status";
            _actualMessages = new List<string>();
            _userInterface = new UserInterface(this, this, this, this, this);
            _state = _userInterface.ProcessInstruction();
        }

        [TestCase("Status: ok")]
        [TestCase("Current time: 17:36, 28 December 2015")]
        [TestCase("Messages sent: 35")]
        [TestCase("User connexions: 46")]
        public void Displays_expected_messages(string message) => Assert.That(_actualMessages, Does.Contain(message));

        [Test]
        public void Returns_a_continue_state() => Assert.That(_state, Is.EqualTo(State.Continue));

        public string ReadInstruction() => _instruction;

        public void ShowMessage(string output) => _actualMessages.Add(output);

        public DateTime Now => new DateTime(2015, 12, 28, 17, 36, 00);

        public int CountMessages() => 35;

        public int CountUserConnexions() => 46;
    }
}