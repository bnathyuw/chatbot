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

        [Given(@".* (?:has|have) posted to Chatbot")]
        public void GivenUserHasPostedToChatbot() => _context.SetUpMessages();

        [When(@"I view ([a-zA-Z]*)'s timeline")]
        public void WhenIViewUsersTimeline(string user) => _context.ViewUsersTimeline(user);

        [Then(@"I should see Alice's messages")]
        public void ThenIShouldSeeAlicesMessage() => _context.AssertAlicesMessages();

        [Then(@"I should see Bob's messages")]
        public void ThenIShouldSeeBobsMessages() => _context.AssertBobsMessages();

        [Given(@"Charlie has followed Alice")]
        public void GivenCharlieHasFollowedAlice() => _context.UserFollowsAnother("Charlie", "Alice");

        [When(@"Charlie views his wall")]
        public void WhenCharlieViewsHisWall() => _context.ViewUsersWall("Charlie");

        [Then(@"he should see Alice and Charlie's messages")]
        public void ThenHeShouldSeeAliceAndCharlieSMessages() => _context.AssertAliceAndCharliesMessages();
    }
}
