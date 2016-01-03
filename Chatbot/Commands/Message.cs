using System;

namespace Chatbot.Commands
{
    public struct Message
    {
        public Message(string user, string text, DateTime sentOn)
        {
            User = user;
            Text = text;
            SentOn = sentOn;
        }

        public string Text { get; }
        public DateTime SentOn { get; }
        public string User { get; }
    }
}