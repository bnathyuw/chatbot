using System;
using System.IO;
using Chatbot.Adapters;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class ConsoleIo_InstructionReader_Tests
    {
        private TextReader _stdIn;
        private string _actualInstruction;
        private TextWriter _stdOut;
        private const string ExpectedInstruction = "Expected Instruction";

        [SetUp]
        public void SetUp()
        {
            StubStandardIn();
            StubStandardOut();
            var instructionReader = new ConsoleIo();
            _actualInstruction = instructionReader.ReadInstruction();
        }

        private void StubStandardIn()
        {
            _stdIn = Console.In;
            Console.SetIn(new StringReader(ExpectedInstruction));
        }

        private void StubStandardOut()
        {
            _stdOut = Console.Out;
            Console.SetOut(new StringWriter());
        }

        [TearDown]
        public void TearDown()
        {
            RestoreStandardInput();
            RestoreStandardOutput();
        }

        private void RestoreStandardInput() => Console.SetIn(_stdIn);

        private void RestoreStandardOutput() => Console.SetOut(_stdOut);

        [Test]
        public void Reads_instruction_from_standard_input() =>
            Assert.That(_actualInstruction, Is.EqualTo(ExpectedInstruction));
    }
}