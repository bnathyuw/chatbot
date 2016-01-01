using System.Collections.Generic;
using System.Text.RegularExpressions;
using Chatbot.Control;

namespace Chatbot.Commands
{
    public interface IFollowedUserRetriever
    {
        IEnumerable<string> RetrieveFollowedUsers(string user);
    }

    public interface IMultipleUserMessageRetriever
    {
        IEnumerable<Message> RetrieveUsersMessages(IEnumerable<string> users);
    }

    public interface IWallMessageDisplayer
    {
        void DisplayWallMessage(Message message);
    }

    public class WallCommand : ICommand
    {
        private readonly IFollowedUserRetriever _followedUserRetriever;
        private readonly IMultipleUserMessageRetriever _multipleUserMessageRetriever;
        private readonly Regex _regex = new Regex("^(?<user>[A-Za-z]*) wall");
        private readonly IWallMessageDisplayer _wallMessageDisplayer;

        public WallCommand(IFollowedUserRetriever followedUserRetriever, IMultipleUserMessageRetriever multipleUserMessageRetriever, IWallMessageDisplayer wallMessageDisplayer)
        {
            _followedUserRetriever = followedUserRetriever;
            _multipleUserMessageRetriever = multipleUserMessageRetriever;
            _wallMessageDisplayer = wallMessageDisplayer;
        }

        public State Do(string command)
        {
            var user = ParseUser(command);
            var messages = GetWallMessages(user);

            foreach (var message in messages)
            {
                _wallMessageDisplayer.DisplayWallMessage(message);
            }

            return State.Continue;
        }

        private string ParseUser(string command) => _regex.Match(command).Groups["user"].Value;

        private IEnumerable<Message> GetWallMessages(string user)
        {
            var users = GetUsersOnWall(user);
            return _multipleUserMessageRetriever.RetrieveUsersMessages(users);
        }

        private IEnumerable<string> GetUsersOnWall(string user)
        {
            yield return user;
            var followedUsers = _followedUserRetriever.RetrieveFollowedUsers(user);
            foreach (var followedUser in followedUsers) yield return followedUser;
        }

        public bool Matches(string command) => _regex.IsMatch(command);
    }
}