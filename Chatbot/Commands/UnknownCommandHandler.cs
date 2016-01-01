using Chatbot.Control;

namespace Chatbot.Commands
{
    public class UnknownCommandHandler : ICommandHandler
    {
        public State Handle(string command)
        {
            return State.Continue;
        }
    }
}