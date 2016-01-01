using System;
using Chatbot.Business;
using Chatbot.Commands;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class MessageAgeFormatter_Tests : IMessageAgeCalculator
    {
        private MessageAgeFormatter _messageAgeFormatter;
        private TimeSpan _age;

        [OneTimeSetUp]
        public void OneTimeSetUp() => _messageAgeFormatter = new MessageAgeFormatter(this);

        [TestCase(1, "1 minute ago")]
        [TestCase(2, "2 minutes ago")]
        [TestCase(15, "15 minutes ago")]
        public void Formats_minutes_correctly(int minutes, string expectedAge)
        {
            _age = TimeSpan.FromMinutes(minutes);
            Assert.That(_messageAgeFormatter.FormatAge(new Message()), Is.EqualTo(expectedAge));
        }

        [TestCase(1, "1 second ago")]
        [TestCase(2, "2 seconds ago")]
        [TestCase(15, "15 seconds ago")]
        public void Formats_seconds_correctly(int seconds, string expectedAge)
        {
            _age = TimeSpan.FromSeconds(seconds);
            Assert.That(_messageAgeFormatter.FormatAge(new Message()), Is.EqualTo(expectedAge));
        }

        public TimeSpan CalculateAge(Message message) => _age;
    }
}