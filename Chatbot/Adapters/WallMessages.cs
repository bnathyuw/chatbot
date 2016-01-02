using System.Collections.Generic;
using Chatbot.Commands;

namespace Chatbot.Adapters
{
    public class WallMessages : IWallMessages
    {
        private readonly IEnumerable<Message> _messages;

        public WallMessages(IEnumerable<Message> messages)
        {
            _messages = messages;
        }

        public void Display(IWallMessageDisplayer wallMessageDisplayer)
        {
            foreach (var message in _messages)
            {
                wallMessageDisplayer.DisplayWallMessage(message);
            }
        }
    }
}