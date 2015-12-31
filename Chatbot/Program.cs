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
            var messageStore = new InMemoryMessages();
            var userConnexionStore = new InMemoryUserConnexions();
            var clockTime = new ClockTime(systemClock);
            var commandHandler = CommandHandler.With(consoleIo, messageStore, userConnexionStore, messageStore, messageStore, messageStore, userConnexionStore, userConnexionStore, clockTime, clockTime, clockTime);
            UserInterface = new UserInterface(consoleIo, commandHandler);
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
