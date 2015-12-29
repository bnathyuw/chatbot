using System;
using Chatbot.Business;

namespace Chatbot.Adapters
{
    public class ConsoleIo : IMessageDisplayer, IInstructionReader
    {
        public void ShowMessage(string output) => Console.WriteLine(output);
        public string ReadInstruction()
        {
            Console.Write("> ");
            return Console.ReadLine();
        }
    }
}