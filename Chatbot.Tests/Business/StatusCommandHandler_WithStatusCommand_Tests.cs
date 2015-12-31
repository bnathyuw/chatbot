using System;
using System.Collections.Generic;
using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class StatusCommandHandler_WithStatusCommand_Tests : IMessageDisplayer, IClock, IMessageCounter, IUserConnexionCounter
    {
        private IList<string> _actualMessages;
        private State _state;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _actualMessages = new List<string>();
            var statusCommandHandler = new StatusCommandHandler(null, this, this, this, new ClockTime(this));
            _state = statusCommandHandler.Handle(SampleCommands.Status);
        }

        [TestCase("Status: ok")]
        [TestCase("Current time: 17:36, 28 December 2015")]
        [TestCase("Messages sent: 35")]
        [TestCase("User connexions: 46")]
        public void Displays_expected_messages(string message) => Assert.That(_actualMessages, Does.Contain(message));

        [Test]
        public void Returns_a_continue_state() => Assert.That(_state, Is.EqualTo(State.Continue));

        public void ShowMessage(string output) => _actualMessages.Add(output);

        public DateTime Now => new DateTime(2015, 12, 28, 17, 36, 0);

        int IMessageCounter.Count() => 35;

        int IUserConnexionCounter.Count() => 46;
    }
}