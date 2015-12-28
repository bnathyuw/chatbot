using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class TimelineInstructionHandler_WithTimelineInstruction_Tests : IMessageDisplayer
    {
        private string _actualMessage;

        [SetUp]
        public void SetUp()
        {
            _actualMessage = null;
            var timelineInstructionHandler = new TimelineInstructionHandler(this, null);
            timelineInstructionHandler.HandleInstruction("Alice");
        }

        [Test]
        public void Displays_users_messages() => 
            Assert.That(_actualMessage, Is.EqualTo("I love the weather today (5 minutes ago)"));

        public void ShowMessage(string output)
        {
            _actualMessage = output;
        }
    }
}