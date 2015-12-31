namespace Chatbot.Business
{
    public class UnknownCommandHandler : ICommandHandler
    {
        public State Handle(string command)
        {
            return State.Continue;
        }
    }
}