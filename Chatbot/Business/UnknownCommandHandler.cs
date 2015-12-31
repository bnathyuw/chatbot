namespace Chatbot.Business
{
    public class UnknownCommandHandler : ICommandHandler
    {
        public State HandleCommand(string command)
        {
            return State.Continue;
        }
    }
}