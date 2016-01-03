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
            var messageAgeFormatter = new AgeFormatter(clockTime);
            var formattedMessageDisplayer = new FormattedMessageDisplayer(messageDisplayer, messageAgeFormatter);

            var exitCommand = new ExitCommand();
            var timelineCommand = new TimelineCommand(formattedMessageDisplayer, messages);
            var postCommand = new PostCommand(clockTime, messages);
            var wallCommand = new WallCommand(formattedMessageDisplayer, new WallMessageRetriever(userConnexions, messages));
            var followCommand = new FollowCommand(userConnexions);
            var statusCommand = new StatusCommand(formattedMessageDisplayer, new StatusQuery(clockTime, messages, userConnexions));

            var commandHandler = new CollectionCommandHandler(exitCommand, timelineCommand, postCommand, wallCommand, followCommand, statusCommand);

            return new UserInterface(commandReader, commandHandler);
        }
    }
}