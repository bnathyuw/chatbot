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

        public TestableChatbot(DateTime now)
        {
            _messagesDisplayed = new Queue<string>();
            _now = now;
            var messageStore = new MessageStore();
            messageStore.SaveMessage(new Message { User = "Alice", Text = "I love the weather today", SentOn = _now.AddMinutes(-5) });
            var userConnexionStore = new UserConnexionStore();
            var statusInstructionHandler = InstructionHandler.With(this, this, messageStore, userConnexionStore, messageStore);
            _userInterface = new UserInterface(this, statusInstructionHandler);
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