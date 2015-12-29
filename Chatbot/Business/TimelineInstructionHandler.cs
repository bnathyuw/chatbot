using System;
using System.Collections.Generic;
using System.Linq;
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
        public string User { get; set; }
    }

    public interface IMessageAgeFormatter
    {
        string FormatAge(Message message);
    }

    public class TimelineInstructionHandler : IInstructionHandler
    {
        private readonly IMessageDisplayer _messageDisplayer;
        private readonly IInstructionHandler _successor;
        private readonly IUserMessageRetriever _userMessageRetriever;
        private readonly Regex _regex = new Regex("^[A-Za-z]*$");
        private readonly IMessageAgeFormatter _messageAgeFormatter;

        public TimelineInstructionHandler(IMessageDisplayer messageDisplayer, IInstructionHandler successor, IUserMessageRetriever userMessageRetriever, IMessageAgeFormatter messageAgeFormatter)
        {
            _messageDisplayer = messageDisplayer;
            _successor = successor;
            _userMessageRetriever = userMessageRetriever;
            _messageAgeFormatter = messageAgeFormatter;
        }

        public State HandleInstruction(string instruction)
        {
            if (!CanHandle(instruction))
                return _successor.HandleInstruction(instruction);

            DisplayUsersMessages(instruction);
            return State.Continue;
        }

        private bool CanHandle(string instruction)
        {
            return _regex.IsMatch(instruction) && instruction != "exit" && instruction != "status";
        }

        private void DisplayUsersMessages(string instruction)
        {
            var messages = _userMessageRetriever.RetrieveUserMessages(instruction);
            foreach (var output in messages.Select(FormatMessage))
            {
                _messageDisplayer.ShowMessage(output);
            }
        }

        private string FormatMessage(Message message)
        {
            return $"{message.Text} ({_messageAgeFormatter.FormatAge(message)})";
        }
    }
}