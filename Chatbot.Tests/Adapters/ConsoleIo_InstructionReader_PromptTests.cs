using System;
using System.IO;
using System.Text;
using Chatbot.Adapters;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class ConsoleIo_InstructionReader_PromptTests : TextWriter
    {
        private TextWriter _stdOut;
        private string _actualPrompt;
        private TextReader _stdIn;

        [SetUp]
        public void SetUp()
        {
            ImpersonateStandardOut();
            StubStandardIn();
            var messageDisplayer = new ConsoleIo();
            messageDisplayer.ReadInstruction();
        }

        private void ImpersonateStandardOut()
        {
            _stdOut = Console.Out;
            Console.SetOut(this);
        }

        private void StubStandardIn()
        {
            _stdIn = Console.In;
            Console.SetIn(new Reader());
        }

        [TearDown]
        public void TearDown()
        {
            RestoreStandardOut();
            RestoreStandardIn();
        }

        private void RestoreStandardOut() => Console.SetOut(_stdOut);

        private void RestoreStandardIn() => Console.SetIn(_stdIn);

        [Test]
        public void Prompts_for_input() => Assert.That(_actualPrompt, Is.EqualTo("> "));

        public override void Write(string value) => _actualPrompt = value;

        public override Encoding Encoding => Encoding.UTF8;

        private class Reader : TextReader
        {
            public override string ReadLine()
            {
                return "";
            }
        }
    }
}