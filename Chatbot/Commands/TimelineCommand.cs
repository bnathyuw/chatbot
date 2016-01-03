using System.Text.RegularExpressions;
using Chatbot.Control;

namespace Chatbot.Commands
{
    public interface IUserMessageRetriever
    {
        ITimelineMessages RetrieveUserMessages(string user);
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

    public interface ITimelineMessages
    {
        void Display(ITimelineMessageDisplayer timelineMessageDisplayer);
    }

    public class TimelineCommand : ICommand
    {
        private readonly Regex _regex = new Regex("^[A-Za-z]*$");

        private readonly IUserMessageRetriever _userMessageRetriever;
        private readonly ITimelineMessageDisplayer _timelineMessageDisplayer;

        public TimelineCommand(ITimelineMessageDisplayer timelineMessageDisplayer, IUserMessageRetriever userMessageRetriever)
        {
            _timelineMessageDisplayer = timelineMessageDisplayer;
            _userMessageRetriever = userMessageRetriever;
        }

        public bool Matches(string command) => _regex.IsMatch(command) && command != "exit" && command != "status";

        public State Do(string command)
        {
            var messages = _userMessageRetriever.RetrieveUserMessages(command);
            messages.Display(_timelineMessageDisplayer);
            return State.Continue;
        }
    }
}