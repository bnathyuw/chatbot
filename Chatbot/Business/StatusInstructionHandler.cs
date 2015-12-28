using System;

namespace Chatbot.Business
{
    public interface IMessageDisplayer
    {
        void ShowMessage(string output);
    }

    public interface IClock
    {
        DateTime Now { get; }
    }

    public interface IMessageCounter
    {
        int CountMessages();
    }

    public interface IUserConnexionCounter
    {
        int CountUserConnexions();
    }

    public class StatusInstructionHandler : IInstructionHandler
    {
        private readonly IMessageDisplayer _messageDisplayer;
        private readonly IClock _clock;
        private readonly IMessageCounter _messageStore;
        private readonly IUserConnexionCounter _userConnexionStore;
        private readonly IInstructionHandler _successor;

        public StatusInstructionHandler(IMessageDisplayer messageDisplayer, IClock clock, IMessageCounter messageStore, IUserConnexionCounter userConnexionStore, IInstructionHandler successor)
        {
            _messageDisplayer = messageDisplayer;
            _clock = clock;
            _messageStore = messageStore;
            _userConnexionStore = userConnexionStore;
            _successor = successor;
        }

        public State HandleInstruction(string instruction)
        {
            if (!CanHandle(instruction))
                return _successor.HandleInstruction(instruction);

            ShowStatusMessage();
            return State.Continue;
        }

        private static bool CanHandle(string instruction) => instruction == "status";

        private void ShowStatusMessage()
        {
            _messageDisplayer.ShowMessage("Status: ok");
            _messageDisplayer.ShowMessage($"Current time: {_clock.Now:HH:mm, d MMMM yyyy}");
            _messageDisplayer.ShowMessage($"Messages sent: {_messageStore.CountMessages()}");
            _messageDisplayer.ShowMessage($"User connexions: {_userConnexionStore.CountUserConnexions()}");
        }
    }
}