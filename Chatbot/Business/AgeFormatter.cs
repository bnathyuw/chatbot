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

            var timeUnit = GetMostSignificantUnit(timeDifference);

            return timeUnit.ToString();

        }

        private static TimeUnit GetMostSignificantUnit(TimeSpan timeDifference)
        {
            if (timeDifference.Minutes > 0)
                return new TimeUnit(timeDifference.Minutes, "minute");
            return new TimeUnit(timeDifference.Seconds, "second");
        }

        private class TimeUnit
        {
            private readonly int _count;
            private readonly string _unitName;

            public TimeUnit(int count, string unitName)
            {
                _count = count;
                _unitName = unitName;
            }

            public override string ToString() => 
                $"{_count} {PluraliseIfNecessary(_count, _unitName)} ago";

            private static string PluraliseIfNecessary(int count, string unitName) =>
                count == 1 ? unitName : unitName + "s";
        }
    }
}