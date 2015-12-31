using System;
using System.IO;
using Chatbot.Adapters;
using NUnit.Framework;

namespace Chatbot.Tests.Adapters
{
    [TestFixture]
    public class ConsoleIo_CommandReader_Tests
    {
        private TextReader _stdIn;
        private string _actualCommand;
        private TextWriter _stdOut;
        private const string ExpectedCommand = "Expected Command";

        [SetUp]
        public void SetUp()
        {
            StubStandardIn();
            StubStandardOut();
            var commandReader = new ConsoleIo();
            _actualCommand = commandReader.ReadCommand();
        }

        private void StubStandardIn()
        {
            _stdIn = Console.In;
            Console.SetIn(new StringReader(ExpectedCommand));
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
        public void Reads_command_from_standard_input() =>
            Assert.That(_actualCommand, Is.EqualTo(ExpectedCommand));
    }
}