using Chatbot.Adapters;
using Chatbot.Control;

namespace Chatbot
{
    static class Program
    {
        private static readonly UserInterface UserInterface;

        static Program()
        {
            var consoleIo = new ConsoleIo();
            var systemClock = new SystemClock();
            UserInterface = DependencyResolver.CreateUserInterface(systemClock, consoleIo, consoleIo);
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
