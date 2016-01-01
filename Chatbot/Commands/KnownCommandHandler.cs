using Chatbot.Control;

namespace Chatbot.Commands
{
    public class KnownCommandHandler : ICommandHandler
    {
        private readonly ICommandHandler _successor;
        private readonly ICommand _command;

        public KnownCommandHandler(ICommandHandler successor, ICommand command)
        {
            _successor = successor;
            _command = command;
        }

        public State Handle(string command) => 
            _command.Matches(command) ? _command.Do(command) : _successor.Handle(command);
    }
}