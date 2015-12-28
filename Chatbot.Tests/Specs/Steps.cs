using System;
using System.Collections.Generic;
using Chatbot.Business;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Chatbot.Tests.Specs
{
    [Binding]
    public class Steps
    {
        private readonly ChatbotContext _context;

        public Steps(ChatbotContext context)
        {
            _context = context;
        }

        [Given(@"Alice has posted to Chatbot")]
        public void GivenAliceHasPostedToChatbot() => _context.PostAlicesMessages();

        [When(@"I view Alice's timeline")]
        public void WhenIViewAlicesTimeline() => _context.ViewAlicesTimeline();

        [Then(@"I should see Alice's message")]
        public void ThenIShouldSeeAlicesMessage() => _context.AssertAlicesMessages();
    }

    public class ChatbotContext : IMessageDisplayer, IClock, IInstructionReader
    {
        private readonly UserInterface _userInterface;
        private string _nextInstruction;
        private readonly Queue<string> _messageDisplayed;
        private readonly DateTime _referenceTime = new DateTime(2015, 12, 28, 20, 34, 00);
        private DateTime _now;

        public ChatbotContext()
        {
            _messageDisplayed = new Queue<string>();

            var consoleIo = this;
            var systemClock = this;
            var messageStore = new Chatbot.Adapters.MessageStore();
            var userConnexionStore = new Chatbot.Adapters.UserConnexionStore();
            var unknownInstructionHandler = new UnknownInstructionHandler();
            var exitInstructionHandler = new ExitInstructionHandler(unknownInstructionHandler);
            var timelineInstructionHandler = new TimelineInstructionHandler(consoleIo, exitInstructionHandler);
            var statusInstructionHandler = new StatusInstructionHandler(consoleIo, systemClock, messageStore, userConnexionStore, timelineInstructionHandler);
            _userInterface = new UserInterface(consoleIo, statusInstructionHandler);
        }

        public void PostAlicesMessages()
        {
            _now = _referenceTime.AddMinutes(-5);
            _nextInstruction = "Alice -> I love the weather today";
            _userInterface.ProcessNextInstruction();
        }

        public void ViewAlicesTimeline()
        {
            _now = _referenceTime;
            _nextInstruction = "Alice";
            _userInterface.ProcessNextInstruction();
        }

        public void AssertAlicesMessages() => 
            Assert.That(_messageDisplayed.Dequeue(), Is.EqualTo("I love the weather today (5 minutes ago)"));

        public void ShowMessage(string output) => _messageDisplayed.Enqueue(output);

        public DateTime Now => _now;

        public string ReadInstruction() => _nextInstruction;
    }
}
