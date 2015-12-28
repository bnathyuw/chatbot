using System;
using NUnit.Framework;

namespace Chatbot.Tests.Specs
{
    public class ChatbotContext
    {
        private readonly DateTime _referenceTime = new DateTime(2015, 12, 28, 20, 34, 00);
        private readonly TestableChatbot _testableChatbot;

        public ChatbotContext()
        {
            _testableChatbot = new TestableChatbot();
        }

        public void PostAlicesMessages()
        {
            _testableChatbot.ProcessInstruction(_referenceTime.AddMinutes(-5), "Alice -> I love the weather today");
        }

        public void ViewAlicesTimeline()
        {
            _testableChatbot.ProcessInstruction(_referenceTime, "Alice");
        }

        public void AssertAlicesMessages() => 
            Assert.That(_testableChatbot.GetMessage(), Is.EqualTo("I love the weather today (5 minutes ago)"));
    }
}