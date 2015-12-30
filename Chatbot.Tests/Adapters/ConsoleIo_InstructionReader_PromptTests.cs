using System;
using System.IO;
using Chatbot.Adapters;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class ConsoleIo_InstructionReader_PromptTests
    {
        private TextWriter _stdOut;
        private TextReader _stdIn;
        private StringWriter _testOut;

        [SetUp]
        public void SetUp()
        {
            InterceptStandardOut();
            StubStandardIn();
            var messageDisplayer = new ConsoleIo();
            messageDisplayer.ReadInstruction();
        }

        private void InterceptStandardOut()
        {
            _stdOut = Console.Out;
            _testOut = new StringWriter();
            Console.SetOut(_testOut);
        }

        private void StubStandardIn()
        {
            _stdIn = Console.In;
            Console.SetIn(new StringReader(""));
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
        public void Prompts_for_input() => Assert.That(_testOut.ToString(), Is.EqualTo("> "));
    }
}