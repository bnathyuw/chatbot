using System.Text.RegularExpressions;
using Chatbot.Control;

namespace Chatbot.Commands
{
    public struct UserConnexion
    {
        public string Follower { get; }
        public string Followed { get; }

        public UserConnexion(string follower, string followed)
        {
            Follower = follower;
            Followed = followed;
        }
    }

    public interface IUserConnexionSaver
    {
        void Save(UserConnexion userConnexion);
    }

    public class FollowCommand : ICommand
    {
        private readonly Regex _regex = new Regex("^(?<follower>[a-zA-Z]*) follows (?<followed>[a-zA-Z]*)$");

        private readonly IUserConnexionSaver _userConnexionSaver;

        public FollowCommand(IUserConnexionSaver userConnexionSaver)
        {
            _userConnexionSaver = userConnexionSaver;
        }

        public State Do(string command)
        {
            var userConnexion = ParseUserConnexion(command);
            _userConnexionSaver.Save(userConnexion);
            return State.Continue;
        }

        private UserConnexion ParseUserConnexion(string command)
        {
            var follower = _regex.Match(command).Groups["follower"].Value;
            var followed = _regex.Match(command).Groups["followed"].Value;

            return new UserConnexion(follower, followed);
        }

        public bool Matches(string command) => _regex.IsMatch(command);
    }
}