using Chatbot.Adapters;
using Chatbot.Business;
using Chatbot.Commands;
using Chatbot.Control;

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
            var exitCommand = new ExitCommand();
            var exitCommandHandler = new KnownCommandHandler(unknownCommandHandler, exitCommand);
            var timelineCommand = new TimelineCommand(formattedMessageDisplayer, messages);
            var timelineCommandHandler = new KnownCommandHandler(exitCommandHandler, timelineCommand);
            var postCommand = new PostCommand(clockTime, messages);
            var postCommandHandler = new KnownCommandHandler(timelineCommandHandler, postCommand);
            var wallCommand = new WallCommand(userConnexions, messages, formattedMessageDisplayer);
            var wallCommandHandler = new KnownCommandHandler(postCommandHandler, wallCommand);
            var followCommand = new FollowCommand(userConnexions);
            var followCommandHandler = new KnownCommandHandler(wallCommandHandler, followCommand);
            var statusCommand = new StatusCommand(formattedMessageDisplayer, clockTime, messages, userConnexions);
            var commandHandler = new KnownCommandHandler(followCommandHandler, statusCommand);

            return new UserInterface(commandReader, commandHandler);
        }
    }
}