using Chatbot.Commands;
using Chatbot.Control;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class WallCommand_Behaviour_Tests : IWallMessageRetriever, IWallMessages
    {
        private string _actualUser;
        private State _actualState;
        private IWallMessageDisplayer _expectedMessageDisplayer;
        private IWallMessageDisplayer _actualMessageDisplayer;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _actualUser = null;
            _expectedMessageDisplayer = new TestWallMessageDisplayer();
            var wallCommand = new WallCommand(_expectedMessageDisplayer, this);
            _actualState = wallCommand.Do("Daphne wall");
        }

        [Test]
        public void Retrieves_wall_messages() => Assert.That(_actualUser, Is.EqualTo("Daphne"));

        [Test]
        public void Displays_the_messages() =>
            Assert.That(_actualMessageDisplayer, Is.EqualTo(_expectedMessageDisplayer));

        [Test]
        public void Returns_continue_state() => Assert.That(_actualState, Is.EqualTo(State.Continue));

        public IWallMessages GetWallMessages(string user)
        {
            _actualUser = user;
            return this;
        }

        public void Display(IWallMessageDisplayer wallMessageDisplayer)
        {
            _actualMessageDisplayer = wallMessageDisplayer;
        }

        private class TestWallMessageDisplayer : IWallMessageDisplayer
        {
            public void DisplayWallMessage(Message message)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}