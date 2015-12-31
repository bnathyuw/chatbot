using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Chatbot.Business
{
    public interface IUserMessageRetriever
    {
        IEnumerable<Message> RetrieveUserMessages(string user);
    }

    public interface IMessageAgeFormatter
    {
        string FormatAge(Message message);
    }

    public class TimelineCommandHandler : ICommandHandler
    {
        private readonly IMessageDisplayer _messageDisplayer;
        private readonly ICommandHandler _successor;
        private readonly IUserMessageRetriever _userMessageRetriever;
        private readonly Regex _regex = new Regex("^[A-Za-z]*$");
        private readonly IMessageAgeFormatter _messageAgeFormatter;

        public TimelineCommandHandler(IMessageDisplayer messageDisplayer, ICommandHandler successor, IUserMessageRetriever userMessageRetriever, IMessageAgeFormatter messageAgeFormatter)
        {
            _messageDisplayer = messageDisplayer;
            _successor = successor;
            _userMessageRetriever = userMessageRetriever;
            _messageAgeFormatter = messageAgeFormatter;
        }

        public State HandleCommand(string command)
        {
            if (!CanHandle(command))
                return _successor.HandleCommand(command);

            DisplayUsersMessages(command);
            return State.Continue;
        }

        private bool CanHandle(string command)
        {
            return _regex.IsMatch(command) && command != "exit" && command != "status";
        }

        private void DisplayUsersMessages(string command)
        {
            var messages = _userMessageRetriever.RetrieveUserMessages(command);
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