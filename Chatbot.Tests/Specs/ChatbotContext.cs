using System;
using Chatbot.Tests.Time;
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
            _testableChatbot.ProcessInstruction("Charlie -> I'm in New York today! Anyone want to have a coffee?", 2.SecondsAgo());
        }

        public void ViewUsersTimeline(string user) => _testableChatbot.ProcessInstruction(user);

        public void UserFollowsAnother(string follower, string followed) => 
            _testableChatbot.ProcessInstruction($"{follower} follows {followed}");

        public void ViewUsersWall(string user) =>
            _testableChatbot.ProcessInstruction($"{user} wall");

        public void ViewUsersWallLater(string user) =>
            _testableChatbot.ProcessInstruction($"{user} wall", 13.SecondsLater());

        public void AssertAlicesMessages() =>
            Assert.That(_testableChatbot.GetMessage(), Is.EqualTo("I love the weather today (5 minutes ago)"));

        public void AssertBobsMessages()
        {
            Assert.That(_testableChatbot.GetMessage(), Is.EqualTo("Good game though. (1 minute ago)"));
            Assert.That(_testableChatbot.GetMessage(), Is.EqualTo("Damn! We lost! (2 minutes ago)"));
        }

        public void AssertAliceAndCharliesMessages()
        {
            Assert.That(_testableChatbot.GetMessage(), Is.EqualTo("Charlie - I'm in New York today! Anyone want to have a coffee? (2 seconds ago)"));
            Assert.That(_testableChatbot.GetMessage(), Is.EqualTo("Alice - I love the weather today (5 minutes ago)"));
        }

        public void AssertAliceBobAndCharliesMessages()
        {
            Assert.That(_testableChatbot.GetMessage(), Is.EqualTo("Charlie - I'm in New York today! Anyone want to have a coffee? (15 seconds ago)"));
            Assert.That(_testableChatbot.GetMessage(), Is.EqualTo("Bob - Good game though. (1 minute ago)"));
            Assert.That(_testableChatbot.GetMessage(), Is.EqualTo("Bob - Damn! We lost! (2 minutes ago)"));
            Assert.That(_testableChatbot.GetMessage(), Is.EqualTo("Alice - I love the weather today (5 minutes ago)"));
        }
    }
}