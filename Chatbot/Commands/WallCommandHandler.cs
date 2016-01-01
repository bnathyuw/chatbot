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

    public class WallCommandHandler : ICommandHandler
    {
        private readonly ICommandHandler _successor;
        private readonly IFollowedUserRetriever _followedUserRetriever;
        private readonly IMultipleUserMessageRetriever _multipleUserMessageRetriever;
        private readonly Regex _regex = new Regex("^(?<user>[A-Za-z]*) wall");
        private readonly IWallMessageDisplayer _wallMessageDisplayer;

        public WallCommandHandler(ICommandHandler successor, IFollowedUserRetriever followedUserRetriever, IMultipleUserMessageRetriever multipleUserMessageRetriever, IWallMessageDisplayer wallMessageDisplayer)
        {
            _successor = successor;
            _followedUserRetriever = followedUserRetriever;
            _multipleUserMessageRetriever = multipleUserMessageRetriever;
            _wallMessageDisplayer = wallMessageDisplayer;
        }

        public State Handle(string command)
        {
            var match = _regex.Match(command);
            if(!match.Success)
                return _successor.Handle(command);

            var user = match.Groups["user"].Value;
            var followedUsers = _followedUserRetriever.RetrieveFollowedUsers(user);
            var messages = _multipleUserMessageRetriever.RetrieveUsersMessages(new List<string> {user}.Concat(followedUsers));

            foreach (var message in messages)
            {
                _wallMessageDisplayer.DisplayWallMessage(message);
            }

            return State.Continue;
        }
    }
}