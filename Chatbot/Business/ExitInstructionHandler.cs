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
            if (!CanHandle(instruction))
                return _successor.HandleInstruction(instruction);

            return State.Exit;
        }

        private static bool CanHandle(string instruction) => instruction == "exit";
    }
}