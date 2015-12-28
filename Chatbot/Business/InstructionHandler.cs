namespace Chatbot.Business
{
    public static class InstructionHandler
    {
        public static IInstructionHandler With(IMessageDisplayer messageDisplayer, IClock systemClock, IMessageCounter messageStore, IUserConnexionCounter userConnexionStore)
        {
            var unknownInstructionHandler = new UnknownInstructionHandler();
            var exitInstructionHandler = new ExitInstructionHandler(unknownInstructionHandler);
            var timelineInstructionHandler = new TimelineInstructionHandler(messageDisplayer, exitInstructionHandler);
            return new StatusInstructionHandler(messageDisplayer, systemClock, messageStore, userConnexionStore,
                timelineInstructionHandler);
        }
    }
}