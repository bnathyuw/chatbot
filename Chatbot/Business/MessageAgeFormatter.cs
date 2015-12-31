using System;

namespace Chatbot.Business
{
    public interface IMessageAgeCalculator
    {
        TimeSpan CalculateAge(Message message);
    }

    public class MessageAgeFormatter : IMessageAgeFormatter
    {
        private readonly IMessageAgeCalculator _messageAgeCalculator;

        public MessageAgeFormatter(IMessageAgeCalculator messageAgeCalculator)
        {
            _messageAgeCalculator = messageAgeCalculator;
        }

        public string FormatAge(Message message)
        {
            var timeDifference = _messageAgeCalculator.CalculateAge(message);

            if (timeDifference.Minutes > 0)
                return FormatWithUnit(timeDifference.Minutes, "minute");

            return FormatWithUnit(timeDifference.Seconds, "second");
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