namespace Chatbot.Business
{
    public class UnknownInstructionHandler : IInstructionHandler
    {
        public State HandleInstruction(string instruction)
        {
            return State.Continue;
        }
    }
}