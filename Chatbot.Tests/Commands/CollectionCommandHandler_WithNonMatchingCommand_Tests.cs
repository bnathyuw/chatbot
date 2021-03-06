﻿using Chatbot.Commands;
using Chatbot.Control;
using NUnit.Framework;

namespace Chatbot.Tests.Commands
{
    public class CollectionCommandHandler_WithNonMatchingCommand_Tests : ICommand
    {
        private const string ExpectedCommand = "Command";
        private bool _wasCalled;
        private State _actualState;
        private string _actualCommand;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var collectionCommandHandler = new CollectionCommandHandler(new TestNonMatchingCommand(),
                new TestNonMatchingCommand(), this, new TestNonMatchingCommand());
            _actualState = collectionCommandHandler.Handle(ExpectedCommand);
        }

        [Test]
        public void Checks_if_the_command_matches() => Assert.That(_actualCommand, Is.EqualTo(ExpectedCommand));

        [Test]
        public void Does_not_call_command() => Assert.That(_wasCalled, Is.False);

        [Test]
        public void Returns_continue_state() => Assert.That(_actualState, Is.EqualTo(State.Continue));

        public bool Matches(string command)
        {
            _actualCommand = command;
            return false;
        }

        public State Do(string command)
        {
            _wasCalled = true;
            return State.Exit;
        }

        private class TestNonMatchingCommand : ICommand
        {
            public bool Matches(string command) => false;

            public State Do(string command)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}