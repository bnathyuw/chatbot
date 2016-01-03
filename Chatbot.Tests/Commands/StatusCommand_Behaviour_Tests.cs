using Chatbot.Commands;
using Chatbot.Control;
using Chatbot.Tests.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class StatusCommand_Behaviour_Tests : IStatusDisplayer, IStatusQuery
    {
        private const string ExpectedTime = "17:36, 28 December 2015";
        private const int ExpectedMessageCount = 35;
        private const int ExpectedConnexionCount = 46;
        private State _state;
        private Status _actualStatus;
        private readonly Status _expectedStatus = new Status(ExpectedTime, ExpectedMessageCount, ExpectedConnexionCount);

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var statusCommand = new StatusCommand(this, this);
            _state = statusCommand.Do(SampleCommands.Status);
        }

        [Test]
        public void Displays_status_from_query() => Assert.That(_actualStatus, Is.EqualTo(_expectedStatus));

        [Test]
        public void Returns_a_continue_state() => Assert.That(_state, Is.EqualTo(State.Continue));

        public void DisplayStatus(Status status) => _actualStatus = status;

        public Status GetStatus() => _expectedStatus;
    }
}