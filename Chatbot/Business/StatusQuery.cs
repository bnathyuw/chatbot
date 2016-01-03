using Chatbot.Commands;

namespace Chatbot.Business
{
    public interface IMessageCounter
    {
        int Count();
    }

    public interface IUserConnexionCounter
    {
        int Count();
    }

    public interface ITimeDisplayer
    {
        string Display { get; }
    }

    public class StatusQuery : IStatusQuery
    {
        private readonly IMessageCounter _messageStore;
        private readonly IUserConnexionCounter _userConnexionStore;
        private readonly ITimeDisplayer _timeDisplayer;

        public StatusQuery(ITimeDisplayer timeDisplayer, IMessageCounter messageStore, IUserConnexionCounter userConnexionStore)
        {
            _timeDisplayer = timeDisplayer;
            _messageStore = messageStore;
            _userConnexionStore = userConnexionStore;
        }

        public Status GetStatus()
        {
            var time = _timeDisplayer.Display;
            var messageCount = _messageStore.Count();
            var userConnexionCount = _userConnexionStore.Count();
            return new Status(time, messageCount, userConnexionCount);
        }
    }
}