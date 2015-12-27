using Chatbot.Business;
using static System.Console;

namespace Chatbot.Adapters
{
    public class ConsoleIo : IMessageDisplayer, IInstructionReader
    {
        public void ShowMessage(string output) => WriteLine(output);
        public string ReadInstruction() => ReadLine();
    }
}