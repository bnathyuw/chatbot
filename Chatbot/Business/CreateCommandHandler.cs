namespace Chatbot.Business
{
    public static class CreateCommandHandler
    {
        public static ICommandHandler WithAdapters(IMessageDisplayer messageDisplayer, IClock systemClock, IMessageCounter messageCounter, IMessageSaver messageSaver, IUserMessageRetriever userMessageRetriever, IMultipleUserMessageRetriever multipleUserMessageRetriever, IUserConnexionCounter userConnexionCounter, IUserConnexionSaver userConnexionSaver, IFollowedUserRetriever followedUserRetriever)
        {
            var clockTime = new ClockTime(systemClock);
            var messageAgeFormatter = new MessageAgeFormatter(clockTime);
            var formattedMessageDisplayer = new FormattedMessageDisplayer(messageDisplayer, messageAgeFormatter);
            return With(messageCounter, userConnexionCounter, userMessageRetriever,
                messageSaver, multipleUserMessageRetriever, followedUserRetriever, userConnexionSaver, clockTime, clockTime,
                formattedMessageDisplayer, formattedMessageDisplayer, formattedMessageDisplayer);
        }

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