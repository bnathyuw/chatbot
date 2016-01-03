using System;
using System.Collections.Generic;
using Chatbot.Business;
using Chatbot.Commands;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class FormattedMessageDisplayer_Tests : IMessageDisplayer, IAgeFormatter
    {
        private FormattedMessageDisplayer _formattedMessageDisplayer;
        private List<string> _actualMessages;
        private readonly DateTime _dateCreated = new DateTime(2016, 1, 2);

        [OneTimeSetUp]
        public void OneTimeSetUp() => _formattedMessageDisplayer = new FormattedMessageDisplayer(this, this);

        [SetUp]
        public void SetUp() => _actualMessages = new List<string>();

        [TestCase("Status: ok")]
        [TestCase("Current time: time")]
        [TestCase("Messages sent: 12")]
        [TestCase("User connexions: 24")]
        public void Displays_expected_status_messages(string expectedMessage)
        {
            _formattedMessageDisplayer.DisplayStatus(new Status("time", 12, 24));
            Assert.That(_actualMessages, Does.Contain(expectedMessage));
        }

        [Test]
        public void Displays_expected_timeline_message()
        {
            _formattedMessageDisplayer.DisplayTimelineMessage(new Message {Text = "Message 1", SentOn = _dateCreated});
            Assert.That(_actualMessages, Does.Contain("Message 1 (2016 01 02)"));
        }

        [Test]
        public void Displays_expected_wall_message()
        {
            _formattedMessageDisplayer.DisplayWallMessage(new Message {User = "User", Text = "Message 1", SentOn = _dateCreated});
            Assert.That(_actualMessages, Does.Contain("User - Message 1 (2016 01 02)"));
        }

        public void ShowMessage(string output) => _actualMessages.Add(output);

        public string FormatAge(DateTime dateCreated) => $"{dateCreated:yyyy MM dd}";
    }
}