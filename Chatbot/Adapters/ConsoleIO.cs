using System;
using Chatbot.Business;
using Chatbot.Control;

namespace Chatbot.Adapters
{
    public class ConsoleIo : IMessageDisplayer, ICommandReader
    {
        public void ShowMessage(string output) => Console.WriteLine(output);

        public string ReadCommand()
        {
            Console.Write("> ");
            return Console.ReadLine();
        }
    }
}