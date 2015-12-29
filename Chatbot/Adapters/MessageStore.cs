using System.Collections.Generic;
using System.Linq;
using Chatbot.Business;

namespace Chatbot.Adapters
{
    public class MessageStore : IMessageCounter, IUserMessageRetriever, IMessageSaver
    {
        private readonly List<Message> _messages = new List<Message>();

        public int CountMessages() => _messages.Count;

        public IEnumerable<Message> RetrieveUserMessages(string user) => _messages.Where(m => m.User == user).OrderByDescending(m => m.SentOn);

        public void SaveMessage(Message message) => _messages.Add(message);
    }
}