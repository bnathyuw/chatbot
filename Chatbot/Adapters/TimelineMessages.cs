using System.Collections.Generic;
using Chatbot.Commands;

namespace Chatbot.Adapters
{
    public class TimelineMessages : ITimelineMessages
    {
        private readonly IEnumerable<Message> _messages;

        public TimelineMessages(IEnumerable<Message> messages)
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
    }
}