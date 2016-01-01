using Chatbot.Control;

namespace Chatbot.Commands
{
    public class ExitCommand : ICommand
    {
        public State Do(string command)
        {
            return State.Exit;
        }

        public bool Matches(string command) => command == "exit";
    }
}