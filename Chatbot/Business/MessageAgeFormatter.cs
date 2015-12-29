namespace Chatbot.Business
{
    public class MessageAgeFormatter : IMessageAgeFormatter
    {
        private readonly IClock _clock;

        public MessageAgeFormatter(IClock clock)
        {
            _clock = clock;
        }

        public string FormatAge(Message message)
        {
            var timeDifference = _clock.Now - message.SentOn;
            var unit = timeDifference.Minutes == 1 ? "minute" : "minutes";
            return $"{timeDifference.Minutes} {unit} ago";
        }
    }
}