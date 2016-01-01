using System.Text.RegularExpressions;
using Chatbot.Control;

namespace Chatbot.Commands
{
    public interface IUserConnexionSaver
    {
        void Save(string follower, string followed);
    }

    public class FollowCommandHandler : ICommandHandler
    {
        private readonly ICommandHandler _successor;
        private readonly IUserConnexionSaver _userConnexionSaver;
        private readonly Regex _regex = new Regex("^(?<follower>[a-zA-Z]*) follows (?<followed>[a-zA-Z]*)$");

        public FollowCommandHandler(ICommandHandler successor, IUserConnexionSaver userConnexionSaver)
        {
            _successor = successor;
            _userConnexionSaver = userConnexionSaver;
        }

        public State Handle(string command)
        {
            var match = _regex.Match(command);

            if(!match.Success)
                return _successor.Handle(command);

            var follower = match.Groups["follower"].Value;
            var followed = match.Groups["followed"].Value;

            _userConnexionSaver.Save(follower, followed);

            return State.Continue;
        }
    }
}