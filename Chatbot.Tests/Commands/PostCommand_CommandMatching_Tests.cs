using Chatbot.Commands;
using Chatbot.Tests.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class PostCommand_CommandMatching_Tests
    {
        private PostCommand _postCommand;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _postCommand = new PostCommand(null, null);
        }

        [Test]
        public void Matches_post_command() => Assert.That(_postCommand.Matches(SampleCommands.Post), Is.True);

        [TestCase(SampleCommands.Status)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Timeline)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Exit)]
        [TestCase(SampleCommands.Wall)]
        [TestCase(SampleCommands.Follow)]
        public void Does_not_match_other_command(string command) => Assert.That(_postCommand.Matches(command), Is.False);
    }
}