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
            var instructionHandler = InstructionHandler.With(consoleIo, systemClock, messageStore, userConnexionStore, messageStore, messageStore, messageStore, userConnexionStore);
            UserInterface = new UserInterface(consoleIo, instructionHandler);
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
