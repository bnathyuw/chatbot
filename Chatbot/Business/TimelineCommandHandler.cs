using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Chatbot.Business
{
    public interface IUserMessageRetriever
    {
        IEnumerable<Message> RetrieveUserMessages(string user);
    }

   public interface ITimelineMessageDisplayer
    {
        void DisplayTimelineMessage(Message message);
    }

    public class TimelineCommandHandler : ICommandHandler
    {
        private readonly ICommandHandler _successor;
        private readonly IUserMessageRetriever _userMessageRetriever;
        private readonly Regex _regex = new Regex("^[A-Za-z]*$");
        private readonly ITimelineMessageDisplayer _timelineMessageDisplayer;

        public TimelineCommandHandler(ICommandHandler successor, IUserMessageRetriever userMessageRetriever, ITimelineMessageDisplayer timelineMessageDisplayer)
        {
            _timelineMessageDisplayer = timelineMessageDisplayer;
            _successor = successor;
            _userMessageRetriever = userMessageRetriever;
        }

        public State Handle(string command)
        {
            if (!CanHandle(command))
                return _successor.Handle(command);

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
            foreach (var message in messages)
            {
                _timelineMessageDisplayer.DisplayTimelineMessage(message);
            }
        }
    }
}