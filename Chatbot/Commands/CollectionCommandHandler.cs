using System.Linq;
using Chatbot.Control;

namespace Chatbot.Commands
{
    public class CollectionCommandHandler : ICommandHandler
    {
        private readonly ICommand[] _commands;

        public CollectionCommandHandler(params ICommand[] commands)
        {
            _commands = commands;
        }

        public State Handle(string commandString)
        {
            var command = _commands.FirstOrDefault(c => c.Matches(commandString));

            return command?.Do(commandString) ?? State.Continue;
        }
    }
}