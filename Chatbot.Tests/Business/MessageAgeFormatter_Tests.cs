using System;
using Chatbot.Business;
using Chatbot.Tests.Time;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class MessageAgeFormatter_Tests : IClock
    {
        private MessageAgeFormatter _messageAgeFormatter;
        private readonly DateTime _now = new DateTime(2015, 12, 29, 17, 40, 0);

        [OneTimeSetUp]
        public void OneTimeSetUp() => _messageAgeFormatter = new MessageAgeFormatter(this);

        [TestCase(1, "1 minute ago")]
        [TestCase(2, "2 minutes ago")]
        [TestCase(15, "15 minutes ago")]
        public void Formats_minutes_correctly(int minutes, string expectedAge) =>
            Assert.That(_messageAgeFormatter.FormatAge(MessageSent(minutes.MinutesAgo())), Is.EqualTo(expectedAge));

        private Message MessageSent(TimeSpan age) => new Message {SentOn = _now.Add(age)};

        public DateTime Now => _now;
    }
}