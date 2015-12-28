using System;

namespace Chatbot.Business
{
    public interface IInstructionReader
    {
        string ReadInstruction();
    }

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

    public class UserInterface
    {
        private readonly IInstructionReader _instructionReader;
        private readonly IMessageDisplayer _messageDisplayer;
        private readonly IClock _clock;
        private readonly IMessageCounter _messageStore;
        private readonly IUserConnexionCounter _userConnexionStore;

        public UserInterface(IInstructionReader instructionReader, IMessageDisplayer messageDisplayer, IClock clock, IMessageCounter messageStore, IUserConnexionCounter userConnexionStore)
        {
            _instructionReader = instructionReader;
            _messageDisplayer = messageDisplayer;
            _clock = clock;
            _messageStore = messageStore;
            _userConnexionStore = userConnexionStore;
        }

        public State ProcessNextInstruction()
        {
            var instruction = _instructionReader.ReadInstruction();
            return ProcessInstruction(instruction);
        }

        private State ProcessInstruction(string instruction)
        {
            if (instruction == "status")
            {
                return HandleStatusInstruction();
            }
            if (instruction == "exit")
            {
                return HandleExitInstruction();
            }
            return HandleUnknownInstruction();
        }

        private State HandleStatusInstruction()
        {
            _messageDisplayer.ShowMessage("Status: ok");
            _messageDisplayer.ShowMessage($"Current time: {_clock.Now:HH:mm, d MMMM yyyy}");
            _messageDisplayer.ShowMessage($"Messages sent: {_messageStore.CountMessages()}");
            _messageDisplayer.ShowMessage($"User connexions: {_userConnexionStore.CountUserConnexions()}");
            return State.Continue;
        }

        private static State HandleExitInstruction()
        {
            return State.Exit;
        }

        private static State HandleUnknownInstruction()
        {
            return State.Continue;
        }
    }
}