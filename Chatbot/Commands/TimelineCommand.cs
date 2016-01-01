using System.Collections.Generic;
using System.Text.RegularExpressions;
using Chatbot.Control;

namespace Chatbot.Commands
{
    public interface IUserMessageRetriever
    {
        IEnumerable<Message> RetrieveUserMessages(string user);
    }

   public interface ITimelineMessageDisplayer
    {
        void DisplayTimelineMessage(Message message);
    }

    public interface ICommand
    {
        bool Matches(string command);
        State Do(string command);
    }

    public class TimelineCommand : ICommand
    {
        private readonly IUserMessageRetriever _userMessageRetriever;
        private readonly Regex _regex = new Regex("^[A-Za-z]*$");
        private readonly ITimelineMessageDisplayer _timelineMessageDisplayer;

        public TimelineCommand(ITimelineMessageDisplayer timelineMessageDisplayer, IUserMessageRetriever userMessageRetriever)
        {
            _timelineMessageDisplayer = timelineMessageDisplayer;
            _userMessageRetriever = userMessageRetriever;
        }

        public bool Matches(string command)
        {
            return _regex.IsMatch(command) && command != "exit" && command != "status";
        }

        public State Do(string command)
        {
            var messages = _userMessageRetriever.RetrieveUserMessages(command);
            foreach (var message in messages)
            {
                _timelineMessageDisplayer.DisplayTimelineMessage(message);
            }
            return State.Continue;
        }
    }
}