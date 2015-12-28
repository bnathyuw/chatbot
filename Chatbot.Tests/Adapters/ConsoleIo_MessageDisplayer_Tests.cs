using System;
using System.IO;
using System.Text;
using Chatbot.Adapters;
using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class ConsoleIo_MessageDisplayer_Tests : TextWriter
    {
        private TextWriter _stdOut;
        private IMessageDisplayer _messageDisplayer;
        private string _actualMessage;
        private const string ExpectedMessage = "Expected Message";

        [SetUp]
        public void SetUp()
        {
            ImpersonateStandardOut();
            _messageDisplayer = new ConsoleIo();
        }

        private void ImpersonateStandardOut()
        {
            _stdOut = Console.Out;
            Console.SetOut(this);
        }

        [TearDown]
        public void TearDown() => RestoreStandardOut();

        private void RestoreStandardOut() => Console.SetOut(_stdOut);

        [Test]
        public void Displays_message_on_standard_out()
        {
            _messageDisplayer.ShowMessage(ExpectedMessage);
            Assert.That(_actualMessage, Is.EqualTo(ExpectedMessage));
        }

        public override void WriteLine(string value) => _actualMessage = value;

        public override Encoding Encoding => Encoding.UTF8;
    }
}