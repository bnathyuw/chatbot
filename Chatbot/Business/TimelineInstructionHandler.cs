using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Chatbot.Business
{
    public interface IUserMessageRetriever
    {
        IEnumerable<Message> RetrieveUserMessages(string user);
    }

    public struct Message
    {
        public string Text { get; set; }
        public DateTime SentOn { get; set; }
    }


    public class TimelineInstructionHandler : IInstructionHandler
    {
        private readonly IMessageDisplayer _messageDisplayer;
        private readonly IInstructionHandler _successor;
        private readonly IUserMessageRetriever _userMessageRetriever;
        private readonly Regex _regex = new Regex("^[A-Za-z]*$");
        private readonly IClock _clock;

        public TimelineInstructionHandler(IMessageDisplayer messageDisplayer, IInstructionHandler successor, IUserMessageRetriever userMessageRetriever, IClock clock)
        {
            _messageDisplayer = messageDisplayer;
            _successor = successor;
            _userMessageRetriever = userMessageRetriever;
            _clock = clock;
        }

        public State HandleInstruction(string instruction)
        {
            if (!CanHandle(instruction))
                return _successor.HandleInstruction(instruction);

            var messages = _userMessageRetriever.RetrieveUserMessages(instruction);
            foreach (var message in messages)
            {
                var timeDifference = _clock.Now - message.SentOn;
                _messageDisplayer.ShowMessage($"{message.Text} ({timeDifference.Minutes} minutes ago)");
            }
            return State.Exit;
        }

        private bool CanHandle(string instruction)
        {
            return _regex.IsMatch(instruction) && instruction != "exit" && instruction != "status";
        }
    }
}