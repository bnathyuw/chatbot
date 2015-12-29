using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Chatbot.Tests
{
    [TestFixture]
    public class SmokeTests
    {
        private Process _process;

        [SetUp]
        public void SetUp()
        {
            var startInfo = new ProcessStartInfo(AppContext.BaseDirectory + "/Chatbot.exe")
            {
                CreateNoWindow = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            _process = Process.Start(startInfo);
        }

        [TearDown]
        public void TearDown()
        {
            _process.Kill();
            _process.Dispose();
        }

        [Test]
        public void Shows_status()
        {
            _process.StandardInput.WriteLine("status");
            Assert.That(_process.StandardOutput.ReadLine(), Does.Match("Status: ok"));
            Assert.That(_process.StandardOutput.ReadLine(), Does.Match("Current time: "));
            Assert.That(_process.StandardOutput.ReadLine(), Does.Match("Messages sent: "));
            Assert.That(_process.StandardOutput.ReadLine(), Does.Match("User connexions: "));
        }
    }
}
