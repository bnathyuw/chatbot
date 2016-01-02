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
            return new Messages(messages);
        }

        public void SaveMessage(Message message) => _messages.Add(message);

        public IWallMessages RetrieveUsersMessages(ITimelineUsers timelineUsers)
        {
            var messages = _messages.Where(m => timelineUsers.Contains(m.User)).OrderByDescending(m => m.SentOn);
            return new Messages(messages);
        }

        private class Messages : ITimelineMessages, IWallMessages
        {
            private readonly IEnumerable<Message> _messages;

            public Messages(IEnumerable<Message> messages)
            {
                _messages = messages;
            }

            public void Display(ITimelineMessageDisplayer timelineMessageDisplayer)
            {
                foreach (var message in _messages)
                {
                    timelineMessageDisplayer.DisplayTimelineMessage(message);
                }
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
}