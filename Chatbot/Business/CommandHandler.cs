namespace Chatbot.Business
{
    public static class CommandHandler
    {
        public static ICommandHandler With(IMessageDisplayer messageDisplayer, IClock clock, IMessageCounter messageCounter, IUserConnexionCounter userConnexionCounter, IUserMessageRetriever userMessageRetriever, IMessageSaver messageSaver, IMultipleUserMessageRetriever multipleUserMessageRetriever, IFollowedUserRetriever followedUserRetriever, IUserConnexionSaver userConnexionSaver)
        {
            var unknownCommandHandler = new UnknownCommandHandler();
            var exitCommandHandler = new ExitCommandHandler(unknownCommandHandler);
            var messageAgeFormatter = new MessageAgeFormatter(new ClockTime(clock));
            var timelineCommandHandler = new TimelineCommandHandler(exitCommandHandler,
                messageDisplayer, userMessageRetriever, messageAgeFormatter);
            var postCommandHandler = new PostCommandHandler(timelineCommandHandler, messageSaver, new ClockTime(clock));
            var wallCommandHandler = new WallCommandHandler(postCommandHandler, followedUserRetriever,
                multipleUserMessageRetriever, messageDisplayer, messageAgeFormatter);
            var followCommandHandler = new FollowCommandHandler(wallCommandHandler, userConnexionSaver);
            return new StatusCommandHandler(followCommandHandler, messageDisplayer, messageCounter, userConnexionCounter, new ClockTime(clock));
        }
    }
}