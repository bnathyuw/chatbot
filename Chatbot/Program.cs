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
            var messageStore = new MessageStore();
            var userConnexionStore = new UserConnexionStore();
            UserInterface = new UserInterface(consoleIo, consoleIo, systemClock, messageStore, userConnexionStore);
        }

        static void Main()
        {
            State state;
            do
            {
                state = UserInterface.ProcessNextInstruction();
            } while (state == State.Continue);
        }
    }
}
