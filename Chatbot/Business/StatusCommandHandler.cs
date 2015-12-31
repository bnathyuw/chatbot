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

    public class StatusCommandHandler : ICommandHandler
    {
        private readonly IMessageDisplayer _messageDisplayer;
        private readonly IClock _clock;
        private readonly IMessageCounter _messageStore;
        private readonly IUserConnexionCounter _userConnexionStore;
        private readonly ICommandHandler _successor;

        public StatusCommandHandler(IMessageDisplayer messageDisplayer, IClock clock, IMessageCounter messageStore, IUserConnexionCounter userConnexionStore, ICommandHandler successor)
        {
            _messageDisplayer = messageDisplayer;
            _clock = clock;
            _messageStore = messageStore;
            _userConnexionStore = userConnexionStore;
            _successor = successor;
        }

        public State HandleCommand(string command)
        {
            if (!CanHandle(command))
                return _successor.HandleCommand(command);

            ShowStatusMessage();
            return State.Continue;
        }

        private static bool CanHandle(string command) => command == "status";

        private void ShowStatusMessage()
        {
            _messageDisplayer.ShowMessage("Status: ok");
            _messageDisplayer.ShowMessage($"Current time: {_clock.Now:HH:mm, d MMMM yyyy}");
            _messageDisplayer.ShowMessage($"Messages sent: {_messageStore.Count()}");
            _messageDisplayer.ShowMessage($"User connexions: {_userConnexionStore.Count()}");
        }
    }
}