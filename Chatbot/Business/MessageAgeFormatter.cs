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

            string unit;
            if (timeDifference.Minutes > 0)
            {
                unit = timeDifference.Minutes == 1 ? "minute" : "minutes";
                return $"{timeDifference.Minutes} {unit} ago";
            }
            unit = timeDifference.Seconds == 1 ? "second" : "seconds";
            return $"{timeDifference.Seconds} {unit} ago";
        }
    }
}