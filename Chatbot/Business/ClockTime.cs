using System;
using Chatbot.Commands;

namespace Chatbot.Business
{
    public interface IClock
    {
        DateTime Now { get; }
    }

    public class ClockTime : IAgeCalculator, ITimestamper, ITimeDisplayer
    {
        private readonly IClock _clock;

        public ClockTime(IClock clock)
        {
            _clock = clock;
        }

        public TimeSpan CalculateAge(DateTime dateCreated) => _clock.Now - dateCreated;

        public DateTime Timestamp => _clock.Now;

        public string Display => _clock.Now.ToString("HH:mm, d MMMM yyyy");
    }
}