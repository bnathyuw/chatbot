using Chatbot.Business;
using Chatbot.Commands;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class StatusQuery_Tests : IMessageCounter, IUserConnexionCounter, ITimeDisplayer
    {
        private const string ExpectedTime = "17:36, 28 December 2015";
        private const int ExpectedMessageCount = 35;
        private const int ExpectedConnexionCount = 46;
        private Status _status;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var statusQuery = new StatusQuery(this, this, this);
            _status = statusQuery.GetStatus();
        }

        [Test]
        public void Returns_status_with_expected_values() =>
            Assert.That(_status, Is.EqualTo(new Status(ExpectedTime, ExpectedMessageCount, ExpectedConnexionCount)));

        int IMessageCounter.Count() => ExpectedMessageCount;

        int IUserConnexionCounter.Count() => ExpectedConnexionCount;

        string ITimeDisplayer.Display => ExpectedTime;
    }
}