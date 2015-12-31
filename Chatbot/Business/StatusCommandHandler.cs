namespace Chatbot.Business
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

    public class StatusCommandHandler : ICommandHandler
    {
        private readonly IMessageDisplayer _messageDisplayer;
        private readonly IMessageCounter _messageStore;
        private readonly IUserConnexionCounter _userConnexionStore;
        private readonly ICommandHandler _successor;
        private readonly ITimeDisplayer _timeDisplayer;

        public StatusCommandHandler(ICommandHandler successor, IMessageDisplayer messageDisplayer, IMessageCounter messageStore, IUserConnexionCounter userConnexionStore, ClockTime timeDisplayer)
        {
            _messageDisplayer = messageDisplayer;
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
            _messageDisplayer.ShowMessage("Status: ok");
            _messageDisplayer.ShowMessage($"Current time: {_timeDisplayer.Display}");
            _messageDisplayer.ShowMessage($"Messages sent: {_messageStore.Count()}");
            _messageDisplayer.ShowMessage($"User connexions: {_userConnexionStore.Count()}");
        }
    }
}