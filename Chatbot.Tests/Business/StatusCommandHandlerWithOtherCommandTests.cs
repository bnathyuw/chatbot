﻿using Chatbot.Business;
using NUnit.Framework;

namespace Chatbot.Tests.Business
{
    [TestFixture]
    public class StatusCommandHandlerWithOtherCommandTests : ICommandHandler
    {
        private string _actualCommand;
        private StatusCommandHandler _statusCommandHandler;
        private const State ExpectedState = State.Exit;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _actualCommand = null;
            _statusCommandHandler = new StatusCommandHandler(this, null, null, null, null);
        }

        [TestCase(SampleCommands.Exit)]
        [TestCase(SampleCommands.Unknown)]
        [TestCase(SampleCommands.Timeline)]
        [TestCase(SampleCommands.Post)]
        [TestCase(SampleCommands.Follow)]
        [TestCase(SampleCommands.Wall)]
        public void Passes_command_to_successor(string command)
        {
            _statusCommandHandler.HandleCommand(command);
            Assert.That(_actualCommand, Is.EqualTo(command));
        }

        [Test]
        public void Returns_state_from_successor()
        {
            var state = _statusCommandHandler.HandleCommand(SampleCommands.Unknown);
            Assert.That(state, Is.EqualTo(ExpectedState));
        }

        public State HandleCommand(string command)
        {
            _actualCommand = command;
            return ExpectedState;
        }
    }
}