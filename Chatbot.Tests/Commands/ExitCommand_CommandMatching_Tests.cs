using Chatbot.Commands;
using Chatbot.Tests.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class ExitCommand_CommandMatching_Tests
    {
        private ExitCommand _exitCommand;

        [OneTimeSetUp]
        public void OneTimeSetUp() => _exitCommand = new ExitCommand();

        [Test]
        public void Matches_exit_command() => Assert.That(_exitCommand.Matches(SampleCommands.Exit), Is.True);

        [TestCase(SampleCommands.Status)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Timeline)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Follow)]
        [TestCase(SampleCommands.Wall)]
        [TestCase(SampleCommands.Post)]
        public void Does_not_match_other_command(string command) => Assert.That(_exitCommand.Matches(command), Is.False);
    }
}