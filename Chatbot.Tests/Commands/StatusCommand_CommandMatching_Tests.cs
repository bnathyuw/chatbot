using Chatbot.Commands;
using Chatbot.Tests.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class StatusCommand_CommandMatching_Tests
    {
        private StatusCommand _statusCommand;

        [OneTimeSetUp]
        public void OneTimeSetUp() => _statusCommand = new StatusCommand(null, null, null, null);

        [Test]
        public void Matches_status_command() => Assert.That(_statusCommand.Matches(SampleCommands.Status), Is.True);

        [TestCase(SampleCommands.Post)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Timeline)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Exit)]
        [TestCase(SampleCommands.Wall)]
        [TestCase(SampleCommands.Follow)]
        public void Does_not_match_other_command(string command) => Assert.That(_statusCommand.Matches(command), Is.False);
    }
}