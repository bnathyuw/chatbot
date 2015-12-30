using System.Collections.Generic;

namespace Chatbot.Business
{
    public static class InstructionHandler
    {
        public static IInstructionHandler With(IMessageDisplayer messageDisplayer, IClock clock, IMessageCounter messageCounter, IUserConnexionCounter userConnexionCounter, IUserMessageRetriever userMessageRetriever, IMessageSaver messageSaver)
        {
            var unknownInstructionHandler = new UnknownInstructionHandler();
            var exitInstructionHandler = new ExitInstructionHandler(unknownInstructionHandler);
            var messageAgeFormatter = new MessageAgeFormatter(clock);
            var timelineInstructionHandler = new TimelineInstructionHandler(messageDisplayer, exitInstructionHandler, userMessageRetriever, messageAgeFormatter);
            var postInstructionHandler = new PostInstructionHandler(clock, messageSaver, timelineInstructionHandler);
            var wallInstructionHandler = new WallInstructionHandler(postInstructionHandler, new FakeUserFollowListRetriever(), new FakeMultipleUserMessageRetriever(clock), messageDisplayer, messageAgeFormatter);
            return new StatusInstructionHandler(messageDisplayer, clock, messageCounter, userConnexionCounter,
                wallInstructionHandler);
        }

        private class FakeMultipleUserMessageRetriever : IMultipleUserMessageRetriever
        {
            private readonly IClock _clock;

            public FakeMultipleUserMessageRetriever(IClock clock)
            {
                _clock = clock;
            }

            public IEnumerable<Message> RetrieveUsersMessages(IEnumerable<string> users)
            {
                yield return new Message
                {
                    User = "Charlie",
                    Text = "I'm in New York today! Anyone want to have a coffee?",
                    SentOn = _clock.Now.AddSeconds(-2)
                };
                yield return new Message
                {
                    User = "Alice",
                    Text = "I love the weather today",
                    SentOn = _clock.Now.AddMinutes(-5)
                };
            }
        }

        private class FakeUserFollowListRetriever : IUserFollowListRetriever
        {
            public IEnumerable<string> RetrieveUserFollowList(string user)
            {
                yield return "Alice";
            }
        }
    }
}