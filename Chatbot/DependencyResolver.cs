using Chatbot.Adapters;
using Chatbot.Business;

namespace Chatbot
{
    public static class DependencyResolver
    {
        public static UserInterface CreateUserInterface(IClock systemClock, IMessageDisplayer messageDisplayer, ICommandReader commandReader)
        {
            var messages = new InMemoryMessages();
            var userConnexions = new InMemoryUserConnexions();

            var clockTime = new ClockTime(systemClock);
            var messageAgeFormatter = new MessageAgeFormatter(clockTime);
            var formattedMessageDisplayer = new FormattedMessageDisplayer(messageDisplayer, messageAgeFormatter);

            var unknownCommandHandler = new UnknownCommandHandler();
            var exitCommandHandler = new ExitCommandHandler(unknownCommandHandler);
            var timelineCommandHandler = new TimelineCommandHandler(exitCommandHandler, messages, formattedMessageDisplayer);
            var postCommandHandler = new PostCommandHandler(timelineCommandHandler, messages, clockTime);
            var wallCommandHandler = new WallCommandHandler(postCommandHandler, userConnexions, messages, formattedMessageDisplayer);
            var followCommandHandler = new FollowCommandHandler(wallCommandHandler, userConnexions);
            var commandHandler = new StatusCommandHandler(followCommandHandler, messages, userConnexions, clockTime, formattedMessageDisplayer);

            return new UserInterface(commandReader, commandHandler);
        }
    }
}