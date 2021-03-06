using Chatbot.Control;

namespace Chatbot.Commands
{
    public class ExitCommand : ICommand
    {
        public State Do(string command) => State.Exit;

        public bool Matches(string command) => command == "exit";
    }
}