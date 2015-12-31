namespace Chatbot.Business
{
    public interface ICommandReader
    {
        string ReadCommand();
    }

    public enum State
    {
        Continue,
        Exit
    }

    public interface ICommandHandler
    {
        State Handle(string command);
    }

    public class UserInterface
    {
        private readonly ICommandReader _commandReader;
        private readonly ICommandHandler _statusCommandHandler;

        public UserInterface(ICommandReader commandReader, ICommandHandler commandHandler)
        {
            _commandReader = commandReader;
            _statusCommandHandler = commandHandler;
        }

        public State ProcessNextCommand()
        {
            var command = _commandReader.ReadCommand();
            return _statusCommandHandler.Handle(command);
        }
    }
}