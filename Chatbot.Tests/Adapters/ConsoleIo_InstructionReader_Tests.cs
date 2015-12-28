using System;
using System.IO;
using Chatbot.Adapters;
using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class ConsoleIo_InstructionReader_Tests : TextReader
    {
        private TextReader _stdIn;
        private IInstructionReader _instructionReader;
        private const string ExpectedInstruction = "Expected Instruction";

        [SetUp]
        public void SetUp()
        {
            ImpersonateStandardInput();
            _instructionReader = new ConsoleIo();
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
            Assert.That(_instructionReader.ReadInstruction(), Is.EqualTo(ExpectedInstruction));

        public override string ReadLine() => ExpectedInstruction;
    }
}