using Chatbot.Commands;

namespace Chatbot.Business
{
    public interface IMessageDisplayer
    {
        void ShowMessage(string output);
    }

    public interface IMessageAgeFormatter
    {
        string FormatAge(Message message);
    }

    public class FormattedMessageDisplayer : IStatusDisplayer, ITimelineMessageDisplayer, IWallMessageDisplayer
    {
        private readonly IMessageDisplayer _messageDisplayer;
        private readonly IMessageAgeFormatter _messageAgeFormatter;

        public FormattedMessageDisplayer(IMessageDisplayer messageDisplayer, IMessageAgeFormatter messageAgeFormatter)
        {
            _messageDisplayer = messageDisplayer;
            _messageAgeFormatter = messageAgeFormatter;
        }

        public void DisplayStatus(string time, int messageCount, int userConnexionCount)
        {
            _messageDisplayer.ShowMessage("Status: ok");
            _messageDisplayer.ShowMessage($"Current time: {time}");
            _messageDisplayer.ShowMessage($"Messages sent: {messageCount}");
            _messageDisplayer.ShowMessage($"User connexions: {userConnexionCount}");
        }

        public void DisplayTimelineMessage(Message message)
        {
            var age = _messageAgeFormatter.FormatAge(message);
            var output = $"{message.Text} ({age})";
            _messageDisplayer.ShowMessage(output);
        }

        public void DisplayWallMessage(Message message)
        {
            var age = _messageAgeFormatter.FormatAge(message);
            var output = $"{message.User} - {message.Text} ({age})";
            _messageDisplayer.ShowMessage(output);
        }
    }
}