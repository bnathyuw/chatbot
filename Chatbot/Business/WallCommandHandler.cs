using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Chatbot.Business
{
    public interface IFollowedUserRetriever
    {
        IEnumerable<string> RetrieveFollowedUsers(string user);
    }

    public interface IMultipleUserMessageRetriever
    {
        IEnumerable<Message> RetrieveUsersMessages(IEnumerable<string> users);
    }

    public class WallCommandHandler : ICommandHandler
    {
        private readonly ICommandHandler _successor;
        private readonly IFollowedUserRetriever _followedUserRetriever;
        private readonly IMultipleUserMessageRetriever _multipleUserMessageRetriever;
        private readonly IMessageDisplayer _messageDisplayer;
        private readonly IMessageAgeFormatter _messageAgeFormatter;
        private readonly Regex _regex = new Regex("^(?<user>[A-Za-z]*) wall");

        public WallCommandHandler(ICommandHandler successor, IFollowedUserRetriever followedUserRetriever, IMultipleUserMessageRetriever multipleUserMessageRetriever, IMessageDisplayer messageDisplayer, IMessageAgeFormatter messageAgeFormatter)
        {
            _successor = successor;
            _followedUserRetriever = followedUserRetriever;
            _multipleUserMessageRetriever = multipleUserMessageRetriever;
            _messageDisplayer = messageDisplayer;
            _messageAgeFormatter = messageAgeFormatter;
        }

        public State HandleCommand(string command)
        {
            var match = _regex.Match(command);
            if(!match.Success)
                return _successor.HandleCommand(command);

            var user = match.Groups["user"].Value;
            var followedUsers = _followedUserRetriever.RetrieveFollowedUsers(user);
            var messages = _multipleUserMessageRetriever.RetrieveUsersMessages(new List<string> {user}.Concat(followedUsers));

            foreach (var message in messages)
            {
                var age = _messageAgeFormatter.FormatAge(message);
                _messageDisplayer.ShowMessage($"{message.User} - {message.Text} ({age})");
            }

            return State.Continue;
        }
    }
}