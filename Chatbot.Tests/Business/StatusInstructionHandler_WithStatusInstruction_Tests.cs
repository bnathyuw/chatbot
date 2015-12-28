using System;
using System.Collections.Generic;
using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class StatusInstructionHandler_WithStatusInstruction_Tests : IMessageDisplayer, IClock, IMessageCounter, IUserConnexionCounter
    {
        private IList<string> _actualMessages;
        private State _state;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _actualMessages = new List<string>();
            var statusInstructionHandler = new StatusInstructionHandler(this, this, this, this, null);
            _state = statusInstructionHandler.HandleInstruction("status");
        }

        [TestCase("Status: ok")]
        [TestCase("Current time: 17:36, 28 December 2015")]
        [TestCase("Messages sent: 35")]
        [TestCase("User connexions: 46")]
        public void Displays_expected_messages(string message) => Assert.That(_actualMessages, Does.Contain(message));

        [Test]
        public void Returns_a_continue_state() => Assert.That(_state, Is.EqualTo(State.Continue));

        public void ShowMessage(string output) => _actualMessages.Add(output);

        public DateTime Now => new DateTime(2015, 12, 28, 17, 36, 00);

        public int CountMessages() => 35;

        public int CountUserConnexions() => 46;
    }
}