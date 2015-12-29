using System;

namespace Chatbot.Business
{
    public struct Message
    {
        public string Text { get; set; }
        public DateTime SentOn { get; set; }
        public string User { get; set; }
    }
}