using System;
using Chatbot.Adapters;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class SystemClock_Tests
    {
        private SystemClock _systemClock;

        [OneTimeSetUp]
        public void OneTimeSetUp() => _systemClock = new SystemClock();

        [Test]
        public void Tells_the_time() => Assert.That(_systemClock.Now, Is.GreaterThan(DateTime.MinValue));
    }
}