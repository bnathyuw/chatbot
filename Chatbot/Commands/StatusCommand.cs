using Chatbot.Control;

namespace Chatbot.Commands
{
    public interface IMessageCounter
    {
        int Count();
    }

    public interface IUserConnexionCounter
    {
        int Count();
    }

    public interface ITimeDisplayer
    {
        string Display { get; }
    }

    public interface IStatusDisplayer
    {
        void DisplayStatus(string time, int messageCount, int userConnexionCount);
    }

    public class StatusCommand : ICommand
    {
        private readonly IMessageCounter _messageStore;
        private readonly IUserConnexionCounter _userConnexionStore;
        private readonly ITimeDisplayer _timeDisplayer;
        private readonly IStatusDisplayer _statusDisplayer;

        public StatusCommand(IStatusDisplayer statusDisplayer, ITimeDisplayer timeDisplayer, IMessageCounter messageStore, IUserConnexionCounter userConnexionStore)
        {
            _statusDisplayer = statusDisplayer;
            _timeDisplayer = timeDisplayer;
            _messageStore = messageStore;
            _userConnexionStore = userConnexionStore;
        }

        public State Do(string command)
        {
            var time = _timeDisplayer.Display;
            var messageCount = _messageStore.Count();
            var userConnexionCount = _userConnexionStore.Count();
            _statusDisplayer.DisplayStatus(time, messageCount, userConnexionCount);

            return State.Continue;
        }

        public bool Matches(string command) => command == "status";
    }
}