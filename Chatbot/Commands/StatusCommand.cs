using Chatbot.Control;

namespace Chatbot.Commands
{
    public struct Status
    {
        public Status(string time, int messageCount, int userConnexionCount)
        {
            Time = time;
            MessageCount = messageCount;
            UserConnexionCount = userConnexionCount;
        }

        public string Time { get; }

        public int MessageCount { get; }

        public int UserConnexionCount { get; }
    }

    public interface IStatusDisplayer
    {
        void DisplayStatus(Status status);
    }

    public interface IStatusQuery
    {
        Status GetStatus();
    }

    public class StatusCommand : ICommand
    {
        private readonly IStatusDisplayer _statusDisplayer;
        private readonly IStatusQuery _statusQuery;

        public StatusCommand(IStatusDisplayer statusDisplayer, IStatusQuery statusQuery)
        {
            _statusDisplayer = statusDisplayer;
            _statusQuery = statusQuery;
        }

        public State Do(string command)
        {
            var status = _statusQuery.GetStatus();
            _statusDisplayer.DisplayStatus(status);

            return State.Continue;
        }

        public bool Matches(string command) => command == "status";
    }
}