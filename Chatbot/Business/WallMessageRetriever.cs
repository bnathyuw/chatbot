using Chatbot.Commands;

namespace Chatbot.Business
{
    public interface IFollowedUserRetriever
    {
        IFollowedUsers RetrieveFollowedUsers(string follower);
    }

    public interface ITimelineUsers
    {
        bool Contains(string user);
    }

    public interface IMultipleUserMessageRetriever
    {
        IWallMessages RetrieveUsersMessages(ITimelineUsers timelineUsers);
    }

    public interface IFollowedUsers
    {
        ITimelineUsers CombineWith(string user);
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
            var followedUsers = _followedUserRetriever.RetrieveFollowedUsers(user);
            var timelineUsers = followedUsers.CombineWith(user);
            return _multipleUserMessageRetriever.RetrieveUsersMessages(timelineUsers);
        }
    }
}