using System;
using System.IO;
using Chatbot.Adapters;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class ConsoleIo_InstructionReader_Tests : TextReader
    {
        private TextReader _stdIn;
        private string _actualInstruction;
        private const string ExpectedInstruction = "Expected Instruction";

        [SetUp]
        public void SetUp()
        {
            ImpersonateStandardInput();
            var instructionReader = new ConsoleIo();
            _actualInstruction = instructionReader.ReadInstruction();
        }

        private void ImpersonateStandardInput()
        {
            _stdIn = Console.In;
            Console.SetIn(this);
        }

        [TearDown]
        public void TearDown() => RestoreStandardInput();

        private void RestoreStandardInput() => Console.SetIn(_stdIn);

        [Test]
        public void Reads_instruction_from_standard_input() =>
            Assert.That(_actualInstruction, Is.EqualTo(ExpectedInstruction));

        public override string ReadLine() => ExpectedInstruction;
    }
}