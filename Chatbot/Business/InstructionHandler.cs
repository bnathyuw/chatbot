namespace Chatbot.Business
{
    public static class InstructionHandler
    {
        public static IInstructionHandler With(IMessageDisplayer messageDisplayer, IClock systemClock, IMessageCounter messageStore, IUserConnexionCounter userConnexionStore, IUserMessageRetriever userMessageRetriever)
        {
            var unknownInstructionHandler = new UnknownInstructionHandler();
            var exitInstructionHandler = new ExitInstructionHandler(unknownInstructionHandler);
            var timelineInstructionHandler = new TimelineInstructionHandler(messageDisplayer, exitInstructionHandler, userMessageRetriever, systemClock);
            return new StatusInstructionHandler(messageDisplayer, systemClock, messageStore, userConnexionStore,
                timelineInstructionHandler);
        }
    }
}