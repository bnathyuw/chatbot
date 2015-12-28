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
            var unknownInstructionHandler = new UnknownInstructionHandler();
            var exitInstructionHandler = new ExitInstructionHandler(unknownInstructionHandler);
            var statusInstructionHandler = new StatusInstructionHandler(consoleIo, systemClock, messageStore, userConnexionStore, exitInstructionHandler);
            UserInterface = new UserInterface(consoleIo, statusInstructionHandler);
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
