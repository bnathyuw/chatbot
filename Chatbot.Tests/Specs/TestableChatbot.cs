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
        private readonly DateTime _referenceTime = new DateTime(2015, 12, 28, 20, 34, 00);

        public TestableChatbot()
        {
            _messagesDisplayed = new Queue<string>();
            var messageStore = new MessageStore();
            messageStore.SaveMessage(new Message { User = "Alice", Text = "I love the weather today", SentOn = _referenceTime.AddMinutes(-5) });
            var userConnexionStore = new UserConnexionStore();
            var statusInstructionHandler = InstructionHandler.With(this, this, messageStore, userConnexionStore, messageStore);
            _userInterface = new UserInterface(this, statusInstructionHandler);
        }

        public void ProcessInstruction(string instruction, TimeSpan? timeDifference = null)
        {
            Now = _referenceTime.Add(timeDifference ?? TimeSpan.Zero);
            _nextInstruction = instruction;
            _userInterface.ProcessNextInstruction();
        }

        public string GetMessage() => _messagesDisplayed.Dequeue();

        public void ShowMessage(string output) => _messagesDisplayed.Enqueue(output);

        public DateTime Now { get; private set; }

        public string ReadInstruction() => _nextInstruction;
    }
}