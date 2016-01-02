using System;
using Chatbot.Business;
using Chatbot.Commands;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class AgeFormatterTests : IAgeCalculator
    {
        private AgeFormatter _ageFormatter;
        private TimeSpan _age;

        [OneTimeSetUp]
        public void OneTimeSetUp() => _ageFormatter = new AgeFormatter(this);

        [TestCase(1, "1 minute ago")]
        [TestCase(2, "2 minutes ago")]
        [TestCase(15, "15 minutes ago")]
        public void Formats_minutes_correctly(int minutes, string expectedAge)
        {
            _age = TimeSpan.FromMinutes(minutes);
            Message message = new Message();
            Assert.That(_ageFormatter.FormatAge(message.SentOn), Is.EqualTo(expectedAge));
        }

        [TestCase(1, "1 second ago")]
        [TestCase(2, "2 seconds ago")]
        [TestCase(15, "15 seconds ago")]
        public void Formats_seconds_correctly(int seconds, string expectedAge)
        {
            _age = TimeSpan.FromSeconds(seconds);
            Message message = new Message();
            Assert.That(_ageFormatter.FormatAge(message.SentOn), Is.EqualTo(expectedAge));
        }

        public TimeSpan CalculateAge(DateTime dateCreated) => _age;
    }
}