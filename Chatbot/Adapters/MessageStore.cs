using System;
using System.Collections.Generic;
using Chatbot.Business;

namespace Chatbot.Adapters
{
    public class MessageStore : IMessageCounter, IUserMessageRetriever
    {
        private readonly IClock _clock;

        public MessageStore(IClock clock)
        {
            _clock = clock;
        }

        public int CountMessages() => 0;
        public IEnumerable<Message> RetrieveUserMessages(string user)
        {
            yield return new Message {Text = "I love the weather today", SentOn = _clock.Now.AddMinutes(-5)};
        }
    }
}