using System.Collections.Generic;
using System.Linq;
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
            var user = _regex.Match(command).Groups["user"].Value;
            var followedUsers = _followedUserRetriever.RetrieveFollowedUsers(user);
            var messages = _multipleUserMessageRetriever.RetrieveUsersMessages(new List<string> {user}.Concat(followedUsers));

            foreach (var message in messages)
            {
                _wallMessageDisplayer.DisplayWallMessage(message);
            }

            return State.Continue;
        }

        public bool Matches(string command)
        {
            return _regex.Match(command).Success;
        }
    }
}