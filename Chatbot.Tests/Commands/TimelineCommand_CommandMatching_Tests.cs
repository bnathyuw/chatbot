using Chatbot.Commands;
using Chatbot.Tests.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    [TestFixture]
    public class TimelineCommand_CommandMatching_Tests
    {
        private TimelineCommand _timelineCommand;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _timelineCommand = new TimelineCommand(null, null);
        }

        [Test]
        public void Matches_timeline_command() => Assert.That(_timelineCommand.Matches(SampleCommands.Timeline), Is.True);

        [TestCase(SampleCommands.Post)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Status)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Exit)]
        [TestCase(SampleCommands.Wall)]
        [TestCase(SampleCommands.Follow)]
        public void Does_not_match_other_command(string command) => Assert.That(_timelineCommand.Matches(command), Is.False);
    }
}