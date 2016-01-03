using System;
using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class AgeFormatter_Tests : IAgeCalculator
    {
        private AgeFormatter _ageFormatter;
        private DateTime _now = new DateTime(2016, 1, 3, 11, 2, 0);

        [OneTimeSetUp]
        public void OneTimeSetUp() => _ageFormatter = new AgeFormatter(this);

        [TestCase(1, "1 minute ago")]
        [TestCase(2, "2 minutes ago")]
        [TestCase(15, "15 minutes ago")]
        public void Formats_minutes_correctly(int minutes, string expectedAge) =>
            Assert.That(_ageFormatter.FormatAge(_now.AddMinutes(-minutes)), Is.EqualTo(expectedAge));

        [TestCase(1, "1 second ago")]
        [TestCase(2, "2 seconds ago")]
        [TestCase(15, "15 seconds ago")]
        public void Formats_seconds_correctly(int seconds, string expectedAge) =>
            Assert.That(_ageFormatter.FormatAge(_now.AddSeconds(-seconds)), Is.EqualTo(expectedAge));

        public TimeSpan CalculateAge(DateTime dateCreated) => _now - dateCreated;
    }
}