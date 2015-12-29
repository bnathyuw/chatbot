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
            return new StatusInstructionHandler(messageDisplayer, clock, messageCounter, userConnexionCounter,
                postInstructionHandler);
        }
    }
}