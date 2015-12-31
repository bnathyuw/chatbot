using System.Collections.Generic;

namespace Chatbot.Business
{
    public static class InstructionHandler
    {
        public static IInstructionHandler With(IMessageDisplayer messageDisplayer, IClock clock, IMessageCounter messageCounter, IUserConnexionCounter userConnexionCounter, IUserMessageRetriever userMessageRetriever, IMessageSaver messageSaver, IMultipleUserMessageRetriever multipleUserMessageRetriever)
        {
            var unknownInstructionHandler = new UnknownInstructionHandler();
            var exitInstructionHandler = new ExitInstructionHandler(unknownInstructionHandler);
            var messageAgeFormatter = new MessageAgeFormatter(clock);
            var timelineInstructionHandler = new TimelineInstructionHandler(messageDisplayer, exitInstructionHandler, userMessageRetriever, messageAgeFormatter);
            var postInstructionHandler = new PostInstructionHandler(clock, messageSaver, timelineInstructionHandler);
            var wallInstructionHandler = new WallInstructionHandler(postInstructionHandler, new FakeFollowedUserRetriever(), multipleUserMessageRetriever, messageDisplayer, messageAgeFormatter);
            return new StatusInstructionHandler(messageDisplayer, clock, messageCounter, userConnexionCounter,
                wallInstructionHandler);
        }

        private class FakeFollowedUserRetriever : IFollowedUserRetriever
        {
            public IEnumerable<string> RetrieveFollowedUsers(string user)
            {
                yield return "Alice";
            }
        }
    }
}