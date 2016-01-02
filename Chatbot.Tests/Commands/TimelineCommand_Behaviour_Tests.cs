using Chatbot.Commands;
using Chatbot.Control;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class TimelineCommand_Behaviour_Tests : IUserMessageRetriever, ITimelineMessages
    {
        private const string ExpectedUser = "Alice";
        private string _actualUser;
        private State _state;
        private ITimelineMessageDisplayer _expectedDisplayer;
        private ITimelineMessageDisplayer _actualDisplayer;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _actualUser = null;
            _expectedDisplayer = new TestTimelineMessageDisplayer();
            var timelineCommand = new TimelineCommand(_expectedDisplayer, this);
            _state = timelineCommand.Do(ExpectedUser);
        }

        [Test]
        public void Retrieves_messages_for_the_user_specified() => Assert.That(_actualUser, Is.EqualTo(ExpectedUser));

        [Test]
        public void Displays_user_messages() => Assert.That(_actualDisplayer, Is.EqualTo(_expectedDisplayer));

        [Test]
        public void Returns_a_continue_state() => Assert.That(_state, Is.EqualTo(State.Continue));

        public ITimelineMessages RetrieveUserMessages(string user)
        {
            _actualUser = user;
            return this;
        }

        public void Display(ITimelineMessageDisplayer timelineMessageDisplayer) => 
            _actualDisplayer = timelineMessageDisplayer;

        private class TestTimelineMessageDisplayer : ITimelineMessageDisplayer
        {
            public void DisplayTimelineMessage(Message message)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}