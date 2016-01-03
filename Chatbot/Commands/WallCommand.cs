using System.Text.RegularExpressions;
using Chatbot.Control;

namespace Chatbot.Commands
{
    public interface IWallMessageDisplayer
    {
        void DisplayWallMessage(Message message);
    }

    public interface IWallMessages
    {
        void Display(IWallMessageDisplayer wallMessageDisplayer);
    }

    public interface IWallMessageRetriever
    {
        IWallMessages GetWallMessages(string user);
    }

    public class WallCommand : ICommand
    {
        private readonly Regex _regex = new Regex("^(?<user>[A-Za-z]*) wall");

        private readonly IWallMessageDisplayer _wallMessageDisplayer;
        private readonly IWallMessageRetriever _wallMessageRetriever;

        public WallCommand(IWallMessageDisplayer wallMessageDisplayer, IWallMessageRetriever wallMessageRetriever)
        {
            _wallMessageRetriever = wallMessageRetriever;
            _wallMessageDisplayer = wallMessageDisplayer;
        }

        public State Do(string command)
        {
            var user = ParseUser(command);
            var wallMessages = _wallMessageRetriever.GetWallMessages(user);
            wallMessages.Display(_wallMessageDisplayer);

            return State.Continue;
        }

        private string ParseUser(string command) => _regex.Match(command).Groups["user"].Value;

        public bool Matches(string command) => _regex.IsMatch(command);
    }
}