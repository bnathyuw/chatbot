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

        [Given(@"[a-zA-Z]* has posted to Chatbot")]
        public void GivenUserHasPostedToChatbot() => _context.SetUpMessages();

        [When(@"I view ([a-zA-Z]*)'s timeline")]
        public void WhenIViewUsersTimeline(string user) => _context.ViewUsersTimeline(user);

        [Then(@"I should see Alice's message")]
        public void ThenIShouldSeeAlicesMessage() => _context.AssertAlicesMessages();

        [Then(@"I should see Bob's messages")]
        public void ThenIShouldSeeBobsMessages() => _context.AssertBobsMessages();
    }
}
