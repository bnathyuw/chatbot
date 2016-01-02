using System.Collections.Generic;
using System.Linq;
using Chatbot.Commands;

namespace Chatbot.Business
{
    public interface IFollowedUserRetriever
    {
        IEnumerable<string> RetrieveFollowedUsers(string follower);
    }

    public interface IMultipleUserMessageRetriever
    {
        IWallMessages RetrieveUsersMessages(IEnumerable<string> users);
    }

    public class WallMessageRetriever : IWallMessageRetriever
    {
        private readonly IFollowedUserRetriever _followedUserRetriever;
        private readonly IMultipleUserMessageRetriever _multipleUserMessageRetriever;

        public WallMessageRetriever(IFollowedUserRetriever followedUserRetriever, IMultipleUserMessageRetriever multipleUserMessageRetriever)
        {
            _followedUserRetriever = followedUserRetriever;
            _multipleUserMessageRetriever = multipleUserMessageRetriever;
        }

        public IWallMessages GetWallMessages(string user)
        {
            var users = GetUsersOnWall(user);
            return _multipleUserMessageRetriever.RetrieveUsersMessages(users);
        }

        private IEnumerable<string> GetUsersOnWall(string user)
        {
            var followedUsers = _followedUserRetriever.RetrieveFollowedUsers(user);
            return new List<string> {user}.Concat(followedUsers);
        }
    }
}