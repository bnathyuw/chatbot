namespace Chatbot.Business
{
    public class ExitInstructionHandler : IInstructionHandler
    {
        private readonly IInstructionHandler _successor;

        public ExitInstructionHandler(IInstructionHandler successor)
        {
            _successor = successor;
        }

        public State HandleInstruction(string instruction)
        {
            if (instruction == "exit")
            {
                return State.Exit;
            }
            return _successor.HandleInstruction(instruction);
        }
    }
}