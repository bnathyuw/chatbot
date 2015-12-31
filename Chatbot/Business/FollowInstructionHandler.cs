namespace Chatbot.Business
{
    public class FollowInstructionHandler : IInstructionHandler
    {
        private readonly IInstructionHandler _successor;

        public FollowInstructionHandler(IInstructionHandler successor)
        {
            _successor = successor;
        }

        public State HandleInstruction(string instruction)
        {
            return _successor.HandleInstruction(instruction);
        }
    }
}