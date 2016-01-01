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

    public class StatusCommandHandler : ICommandHandler
    {
        private readonly IMessageCounter _messageStore;
        private readonly IUserConnexionCounter _userConnexionStore;
        private readonly ICommandHandler _successor;
        private readonly ITimeDisplayer _timeDisplayer;
        private readonly IStatusDisplayer _statusDisplayer;

        public StatusCommandHandler(ICommandHandler successor, IMessageCounter messageStore, IUserConnexionCounter userConnexionStore, ITimeDisplayer timeDisplayer, IStatusDisplayer statusDisplayer)
        {
            _statusDisplayer = statusDisplayer;
            _timeDisplayer = timeDisplayer;
            _messageStore = messageStore;
            _userConnexionStore = userConnexionStore;
            _successor = successor;
        }

        public State Handle(string command)
        {
            if (!CanHandle(command))
                return _successor.Handle(command);

            ShowStatusMessage();
            return State.Continue;
        }

        private static bool CanHandle(string command) => command == "status";

        private void ShowStatusMessage()
        {
            var time = _timeDisplayer.Display;
            var messageCount = _messageStore.Count();
            var userConnexionCount = _userConnexionStore.Count();
            _statusDisplayer.DisplayStatus(time, messageCount, userConnexionCount);
        }
    }
}