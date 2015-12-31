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
        public void OneTimeSetUp() => _messageAgeFormatter = new MessageAgeFormatter(new ClockTime(this));

        [TestCase(1, "1 minute ago")]
        [TestCase(2, "2 minutes ago")]
        [TestCase(15, "15 minutes ago")]
        public void Formats_minutes_correctly(int minutes, string expectedAge) =>
            Assert.That(_messageAgeFormatter.FormatAge(MessageSent(minutes.MinutesAgo())), Is.EqualTo(expectedAge));

        [TestCase(1, "1 second ago")]
        [TestCase(2, "2 seconds ago")]
        [TestCase(15, "15 seconds ago")]
        public void Formats_seconds_correctly(int seconds, string expectedAge) =>
            Assert.That(_messageAgeFormatter.FormatAge(MessageSent(seconds.SecondsAgo())), Is.EqualTo(expectedAge));

        private Message MessageSent(TimeSpan age) => new Message {SentOn = _now.Add(age)};

        public DateTime Now => _now;
    }
}