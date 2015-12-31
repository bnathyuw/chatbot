using System.Text.RegularExpressions;

namespace Chatbot.Business
{
    public interface IUserConnexionSaver
    {
        void SaveConnexion(string follower, string followed);
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

        public State HandleCommand(string command)
        {
            var match = _regex.Match(command);

            if(!match.Success)
                return _successor.HandleCommand(command);

            var follower = match.Groups["follower"].Value;
            var followed = match.Groups["followed"].Value;

            _userConnexionSaver.SaveConnexion(follower, followed);

            return State.Continue;
        }
    }
}