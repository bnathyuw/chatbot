using System;
using System.Collections.Generic;
using Chatbot.Adapters;
using Chatbot.Business;

namespace Chatbot.Tests.Specs
{
    public class TestableChatbot : IMessageDisplayer, IClock, IInstructionReader
    {
        private readonly UserInterface _userInterface;
        private string _nextInstruction;
        private readonly Queue<string> _messagesDisplayed;
        private DateTime _now;

        public TestableChatbot()
        {
            _messagesDisplayed = new Queue<string>();

            var consoleIo = this;
            var systemClock = this;
            var messageStore = new MessageStore();
            var userConnexionStore = new UserConnexionStore();
            var statusInstructionHandler = InstructionHandler.With(consoleIo, systemClock, messageStore, userConnexionStore);
            _userInterface = new UserInterface(consoleIo, statusInstructionHandler);
        }

        public void ProcessInstruction(DateTime time, string instruction)
        {
            _now = time;
            _nextInstruction = instruction;
            _userInterface.ProcessNextInstruction();
        }

        public string GetMessage() => _messagesDisplayed.Dequeue();

        public void ShowMessage(string output) => _messagesDisplayed.Enqueue(output);

        public DateTime Now => _now;

        public string ReadInstruction() => _nextInstruction;
    }
}