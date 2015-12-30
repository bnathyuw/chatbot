using System;

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
            var timeDifference = GetTimeDifference(message);

            if (timeDifference.Minutes > 0)
                return FormatWithUnit(timeDifference.Minutes, "minute");

            return FormatWithUnit(timeDifference.Seconds, "second");
        }

        private TimeSpan GetTimeDifference(Message message)
        {
            return _clock.Now - message.SentOn;
        }

        private static string FormatWithUnit(int count, string unitName)
        {
            return $"{count} {PluraliseIfNecessary(count, unitName)} ago";
        }

        private static string PluraliseIfNecessary(int count, string unitName)
        {
            return count == 1 ? unitName : unitName + "s";
        }
    }
}