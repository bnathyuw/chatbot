namespace Chatbot.Business
{
    public static class CommandHandler
    {
        public static ICommandHandler With(IMessageCounter messageCounter, IUserConnexionCounter userConnexionCounter, IUserMessageRetriever userMessageRetriever, IMessageSaver messageSaver, IMultipleUserMessageRetriever multipleUserMessageRetriever, IFollowedUserRetriever followedUserRetriever, IUserConnexionSaver userConnexionSaver, ClockTime timestamper, ClockTime timeDisplayer, ITimelineMessageDisplayer timelineMessageDisplayer, IWallMessageDisplayer wallMessageDisplayer, IStatusDisplayer statusDisplayer)
        {
            var unknownCommandHandler = new UnknownCommandHandler();
            var exitCommandHandler = new ExitCommandHandler(unknownCommandHandler);
            var timelineCommandHandler = new TimelineCommandHandler(exitCommandHandler, userMessageRetriever, timelineMessageDisplayer);
            var postCommandHandler = new PostCommandHandler(timelineCommandHandler, messageSaver, timestamper);
            var wallCommandHandler = new WallCommandHandler(postCommandHandler, followedUserRetriever,
                multipleUserMessageRetriever, wallMessageDisplayer);
            var followCommandHandler = new FollowCommandHandler(wallCommandHandler, userConnexionSaver);
            return new StatusCommandHandler(followCommandHandler, messageCounter, userConnexionCounter, timeDisplayer, statusDisplayer);
        }
    }
}