using System;
using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class ClockTime_Tests : IClock
    {
        private readonly DateTime _now = new DateTime(2015, 12, 30, 11, 43, 0);
        private ClockTime _clockTime;

        [OneTimeSetUp]
        public void OneTimeSetUp() => _clockTime = new ClockTime(this);

        [Test]
        public void Calculates_age_of_a_message() =>
            Assert.That(_clockTime.CalculateAge(_now.AddMinutes(-10)), Is.EqualTo(TimeSpan.FromMinutes(10)));

        [Test]
        public void Gives_timestamp() => Assert.That(_clockTime.Timestamp, Is.EqualTo(_now));

        [Test]
        public void Displays_the_time() => Assert.That(_clockTime.Display, Is.EqualTo("11:43, 30 December 2015"));

        public DateTime Now => _now;
    }
}