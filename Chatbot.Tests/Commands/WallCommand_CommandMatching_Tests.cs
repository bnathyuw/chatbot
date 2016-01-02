using Chatbot.Commands;
using Chatbot.Tests.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class WallCommand_CommandMatching_Tests
    {
        private WallCommand _wallCommand;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _wallCommand = new WallCommand(null, null);
        }

        [Test]
        public void Matches_wall_command() => Assert.That(_wallCommand.Matches(SampleCommands.Wall), Is.True);

        [TestCase(SampleCommands.Post)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Status)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Exit)]
        [TestCase(SampleCommands.Timeline)]
        [TestCase(SampleCommands.Follow)]
        public void Does_not_match_other_command(string command) => Assert.That(_wallCommand.Matches(command), Is.False);
    }
}