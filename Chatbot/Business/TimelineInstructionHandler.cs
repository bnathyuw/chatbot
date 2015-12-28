using System.Text.RegularExpressions;

namespace Chatbot.Business
{
    public class TimelineInstructionHandler : IInstructionHandler
    {
        private readonly IMessageDisplayer _messageDisplayer;
        private readonly IInstructionHandler _successor;
        private readonly Regex _regex = new Regex("^[A-Za-z]*$");

        public TimelineInstructionHandler(IMessageDisplayer messageDisplayer, IInstructionHandler successor)
        {
            _messageDisplayer = messageDisplayer;
            _successor = successor;
        }

        public State HandleInstruction(string instruction)
        {
            if (!CanHandle(instruction))
                return _successor.HandleInstruction(instruction);
            _messageDisplayer.ShowMessage("I love the weather today (5 minutes ago)");
            return State.Exit;
        }

        private bool CanHandle(string instruction)
        {
            return _regex.IsMatch(instruction) && instruction != "exit" && instruction != "status";
        }
    }
}