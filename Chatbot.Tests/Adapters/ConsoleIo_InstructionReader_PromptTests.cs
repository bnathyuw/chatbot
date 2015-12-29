using System;
using System.IO;
using System.Text;
using Chatbot.Adapters;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class ConsoleIo_InstructionReader_PromptTests : TextWriter { 
        private TextWriter _stdOut;
        private string _actualPrompt;

        [SetUp]
        public void SetUp()
        {
            ImpersonateStandardOut();
            var messageDisplayer = new ConsoleIo();
            messageDisplayer.ReadInstruction();
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
        public void Prompts_for_input() => Assert.That(_actualPrompt, Is.EqualTo("> "));

        public override void Write(string value) => _actualPrompt = value;

        public override Encoding Encoding => Encoding.UTF8;
    }
}