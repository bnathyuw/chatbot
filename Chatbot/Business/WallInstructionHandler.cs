using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Chatbot.Business
{
    public interface IUserFollowListRetriever
    {
        IEnumerable<string> RetrieveUserFollowList(string user);
    }

    public interface IMultipleUserMessageRetriever
    {
        IEnumerable<Message> RetrieveUsersMessages(IEnumerable<string> users);
    }

    public class WallInstructionHandler : IInstructionHandler
    {
        private readonly IInstructionHandler _successor;
        private readonly IUserFollowListRetriever _userFollowListRetriever;
        private readonly IMultipleUserMessageRetriever _multipleUserMessageRetriever;
        private readonly IMessageDisplayer _messageDisplayer;
        private readonly IMessageAgeFormatter _messageAgeFormatter;
        private readonly Regex _regex = new Regex("^(?<user>[A-Za-z]*) wall");

        public WallInstructionHandler(IInstructionHandler successor, IUserFollowListRetriever userFollowListRetriever, IMultipleUserMessageRetriever multipleUserMessageRetriever, IMessageDisplayer messageDisplayer, IMessageAgeFormatter messageAgeFormatter)
        {
            _successor = successor;
            _userFollowListRetriever = userFollowListRetriever;
            _multipleUserMessageRetriever = multipleUserMessageRetriever;
            _messageDisplayer = messageDisplayer;
            _messageAgeFormatter = messageAgeFormatter;
        }

        public State HandleInstruction(string instruction)
        {
            var match = _regex.Match(instruction);
            if(!match.Success)
                return _successor.HandleInstruction(instruction);

            var user = match.Groups["user"].Value;
            var followedUsers = _userFollowListRetriever.RetrieveUserFollowList(user);
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