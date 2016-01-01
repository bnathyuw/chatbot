using System;
using Chatbot.Commands;
using Chatbot.Control;
using Chatbot.Tests.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class StatusCommandHandler_WithStatusCommand_Tests : IMessageCounter, IUserConnexionCounter, ITimeDisplayer, IStatusDisplayer
    {
        private const string ExpectedTime = "17:36, 28 December 2015";
        private const int ExpectedMessageCount = 35;
        private const int ExpectedConnexionCount = 46;
        private State _state;
        private Tuple<string, int, int> _actualValues;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var statusCommandHandler = new StatusCommandHandler(null, this, this, this, this);
            _state = statusCommandHandler.Handle(SampleCommands.Status);
        }

        [Test]
        public void Displays_status_with_expected_values() => 
            Assert.That(_actualValues, Is.EqualTo(new Tuple<string, int, int>(ExpectedTime, ExpectedMessageCount, ExpectedConnexionCount)));

        [Test]
        public void Returns_a_continue_state() => Assert.That(_state, Is.EqualTo(State.Continue));

        int IMessageCounter.Count() => ExpectedMessageCount;

        int IUserConnexionCounter.Count() => ExpectedConnexionCount;

        string ITimeDisplayer.Display => ExpectedTime;

        public void DisplayStatus(string time, int messageCount, int userConnexionCount) => _actualValues = new Tuple<string, int, int>(time, messageCount, userConnexionCount);
    }
}