namespace Chatbot.Business
{
    public class ExitCommandHandler : ICommandHandler
    {
        private readonly ICommandHandler _successor;

        public ExitCommandHandler(ICommandHandler successor)
        {
            _successor = successor;
        }

        public State Handle(string command)
        {
            if (!CanHandle(command))
                return _successor.Handle(command);

            return State.Exit;
        }

        private static bool CanHandle(string command) => command == "exit";
    }
}