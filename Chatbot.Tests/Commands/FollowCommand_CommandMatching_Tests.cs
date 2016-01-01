using Chatbot.Commands;
using Chatbot.Tests.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class FollowCommand_CommandMatching_Tests
    {
        private FollowCommand _followCommand;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _followCommand = new FollowCommand(null);
        }

        [Test]
        public void Matches_follow_command() => Assert.That(_followCommand.Matches(SampleCommands.Follow), Is.True);

        [TestCase(SampleCommands.Status)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Timeline)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Exit)]
        [TestCase(SampleCommands.Post)]
        [TestCase(SampleCommands.Wall)]
        public void Does_not_match_other_command(string command) => Assert.That(_followCommand.Matches(command), Is.False);
    }
}