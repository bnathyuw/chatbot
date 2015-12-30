using System;
using System.IO;
using Chatbot.Adapters;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class ConsoleIo_MessageDisplayer_Tests
    {
        private TextWriter _stdOut;
        private StringWriter _testOut;
        private const string ExpectedMessage = "Expected Message";

        [SetUp]
        public void SetUp()
        {
            InterceptStandardOut();
            var messageDisplayer = new ConsoleIo();
            messageDisplayer.ShowMessage(ExpectedMessage);
        }

        private void InterceptStandardOut()
        {
            _stdOut = Console.Out;
            _testOut = new StringWriter();
            Console.SetOut(_testOut);
        }

        [TearDown]
        public void TearDown() => RestoreStandardOut();

        private void RestoreStandardOut() => Console.SetOut(_stdOut);

        [Test]
        public void Displays_message_on_standard_out() => Assert.That(_testOut.ToString(), Does.StartWith(ExpectedMessage));
    }
}