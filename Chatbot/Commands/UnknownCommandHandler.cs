using Chatbot.Control;

namespace Chatbot.Commands
{
    public class UnknownCommandHandler : ICommandHandler
    {
        public State Handle(string command) => State.Continue;
    }
}