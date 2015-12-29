using NUnit.Framework;

namespace Chatbot.Tests.Specs
{
    public class ChatbotContext
    {
        private readonly TestableChatbot _testableChatbot;

        public ChatbotContext()
        {
            _testableChatbot = new TestableChatbot();
        }

        public void SetUpMessages()
        {
            _testableChatbot.ProcessInstruction("Alice -> I love the weather today", 5.MinutesAgo());
            _testableChatbot.ProcessInstruction("Bob -> Damn! We lost!", 2.MinutesAgo());
            _testableChatbot.ProcessInstruction("Bob -> Good game though.", 1.MinuteAgo());
        }

        public void ViewUsersTimeline(string user) => _testableChatbot.ProcessInstruction(user);

        public void AssertAlicesMessages() =>
            Assert.That(_testableChatbot.GetMessage(), Is.EqualTo("I love the weather today (5 minutes ago)"));

        public void AssertBobsMessages()
        {
            Assert.That(_testableChatbot.GetMessage(), Is.EqualTo("Good game though. (1 minute ago)"));
            Assert.That(_testableChatbot.GetMessage(), Is.EqualTo("Damn! We lost! (2 minutes ago)"));
        }
    }
}