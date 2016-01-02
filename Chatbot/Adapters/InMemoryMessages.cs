using System.Collections.Generic;
using System.Linq;
using Chatbot.Business;
using Chatbot.Commands;

namespace Chatbot.Adapters
{
    public class InMemoryMessages : IMessageCounter, IUserMessageRetriever, IMessageSaver, IMultipleUserMessageRetriever
    {
        private readonly List<Message> _messages = new List<Message>();

        public int Count() => _messages.Count;

        public ITimelineMessages RetrieveUserMessages(string user)
        {
            var messages = _messages.Where(m => m.User == user).OrderByDescending(m => m.SentOn);
            return new TimelineMessages(messages);
        }

        public void SaveMessage(Message message) => _messages.Add(message);

        public IWallMessages RetrieveUsersMessages(IEnumerable<string> users)
        {
            var messages = _messages.Where(m => users.Contains(m.User)).OrderByDescending(m => m.SentOn);
            return new WallMessages(messages);
        }
    }
}