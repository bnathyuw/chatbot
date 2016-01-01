using Chatbot.Adapters;
using Chatbot.Business;

namespace Chatbot
{
    static class Program
    {
        private static readonly UserInterface UserInterface;

        static Program()
        {
            var consoleIo = new ConsoleIo();
            var systemClock = new SystemClock();
            var userInterface = DependencyResolver.CreateUserInterface(systemClock, consoleIo, consoleIo);
            UserInterface = userInterface;
        }

        static void Main()
        {
            State state;
            do
            {
                state = UserInterface.ProcessNextCommand();
            } while (state == State.Continue);
        }
    }
}
