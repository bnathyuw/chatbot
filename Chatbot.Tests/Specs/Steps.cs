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
}
