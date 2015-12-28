using System;
using System.Collections.Generic;
using Chatbot.Business;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Chatbot.Tests
{
    [TestFixture]
    public class UserInterface_StatusInstruction_Tests : IInstructionReader, IMessageDisplayer, IClock, IMessageCounter, IUserConnexionCounter
    {
        private string _instruction;
        private IList<string> _actualMessages;
        private UserInterface _userInterface;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _instruction = "status";
            _actualMessages = new List<string>();
            _userInterface = new UserInterface(this, this, this, this, this);
            _userInterface.ProcessInstruction();
        }

        [Test]
        public void Reports_status_ok() => Assert.That(_actualMessages, Does.Contain("Status: ok"));

        public string ReadInstruction() => _instruction;

        public void ShowMessage(string output) => _actualMessages.Add(output);

        public DateTime Now { get; }

        public int CountMessages() => 35;

        public int CountUserConnexions() => 46;
    }
}