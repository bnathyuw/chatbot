using System.Collections.Generic;
using System.Linq;
using Chatbot.Business;

namespace Chatbot.Adapters
{
    public class InMemoryMessages : IMessageCounter, IUserMessageRetriever, IMessageSaver, IMultipleUserMessageRetriever
    {
        private readonly List<Message> _messages = new List<Message>();

        public int Count() => _messages.Count;

        public IEnumerable<Message> RetrieveUserMessages(string user) =>
            _messages.Where(m => m.User == user).OrderByDescending(m => m.SentOn);

        public void SaveMessage(Message message) => _messages.Add(message);

        public IEnumerable<Message> RetrieveUsersMessages(IEnumerable<string> users) =>
            _messages.Where(m => users.Contains(m.User)).OrderByDescending(m => m.SentOn);
    }
}