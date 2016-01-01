using System;
using System.Collections.Generic;
using Chatbot.Business;
using Chatbot.Control;

namespace Chatbot.Tests.Specs
{
    public class TestableChatbot : IMessageDisplayer, IClock, ICommandReader
    {
        private readonly UserInterface _userInterface;
        private string _nextCommand;
        private readonly Queue<string> _messagesDisplayed;
        private readonly DateTime _referenceTime = new DateTime(2015, 12, 28, 20, 34, 0);

        public TestableChatbot()
        {
            _messagesDisplayed = new Queue<string>();
            _userInterface = DependencyResolver.CreateUserInterface(this, this, this);
        }

        public void ProcessCommand(string command, TimeSpan? timeDifference = null)
        {
            Console.WriteLine($"  > {command}");
            Now = _referenceTime.Add(timeDifference ?? TimeSpan.Zero);
            _nextCommand = command;
            _userInterface.ProcessNextCommand();
        }

        public string GetMessage() => _messagesDisplayed.Dequeue();

        public void ShowMessage(string output)
        {
            Console.WriteLine($"  {output}");
            _messagesDisplayed.Enqueue(output);
        }

        public DateTime Now { get; private set; }

        public string ReadCommand() => _nextCommand;
    }
}