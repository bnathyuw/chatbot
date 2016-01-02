using System;
using Chatbot.Commands;

namespace Chatbot.Business
{
    public interface IMessageDisplayer
    {
        void ShowMessage(string output);
    }

    public interface IAgeFormatter
    {
        string FormatAge(DateTime dateCreated);
    }

    public class FormattedMessageDisplayer : IStatusDisplayer, ITimelineMessageDisplayer, IWallMessageDisplayer
    {
        private readonly IMessageDisplayer _messageDisplayer;
        private readonly IAgeFormatter _ageFormatter;

        public FormattedMessageDisplayer(IMessageDisplayer messageDisplayer, IAgeFormatter ageFormatter)
        {
            _messageDisplayer = messageDisplayer;
            _ageFormatter = ageFormatter;
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
            var age = _ageFormatter.FormatAge(message.SentOn);
            var output = $"{message.Text} ({age})";
            _messageDisplayer.ShowMessage(output);
        }

        public void DisplayWallMessage(Message message)
        {
            var age = _ageFormatter.FormatAge(message.SentOn);
            var output = $"{message.User} - {message.Text} ({age})";
            _messageDisplayer.ShowMessage(output);
        }
    }
}