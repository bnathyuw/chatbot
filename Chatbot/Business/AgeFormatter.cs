using System;

namespace Chatbot.Business
{
    public interface IAgeCalculator
    {
        TimeSpan CalculateAge(DateTime dateCreated);
    }

    public class AgeFormatter : IAgeFormatter
    {
        private readonly IAgeCalculator _ageCalculator;

        public AgeFormatter(IAgeCalculator ageCalculator)
        {
            _ageCalculator = ageCalculator;
        }

        public string FormatAge(DateTime dateCreated)
        {
            var timeDifference = _ageCalculator.CalculateAge(dateCreated);

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