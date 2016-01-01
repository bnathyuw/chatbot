using System.Text.RegularExpressions;
using Chatbot.Control;

namespace Chatbot.Commands
{
    public interface IUserConnexionSaver
    {
        void Save(string follower, string followed);
    }

    public class FollowCommand : ICommand
    {
        private readonly IUserConnexionSaver _userConnexionSaver;
        private readonly Regex _regex = new Regex("^(?<follower>[a-zA-Z]*) follows (?<followed>[a-zA-Z]*)$");

        public FollowCommand(IUserConnexionSaver userConnexionSaver)
        {
            _userConnexionSaver = userConnexionSaver;
        }

        public State Do(string command)
        {
            var follower = _regex.Match(command).Groups["follower"].Value;
            var followed = _regex.Match(command).Groups["followed"].Value;

            _userConnexionSaver.Save(follower, followed);

            return State.Continue;
        }

        public bool Matches(string command)
        {
            return _regex.Match(command).Success;
        }
    }
}