using System;

namespace Chatbot.Business
{
    public interface IClock
    {
        DateTime Now { get; }
    }

    public class ClockTime : IMessageAgeCalculator, ITimestamper, ITimeDisplayer
    {
        private readonly IClock _clock;

        public ClockTime(IClock clock)
        {
            _clock = clock;
        }

        public TimeSpan CalculateAge(Message message) => _clock.Now - message.SentOn;

        public DateTime Timestamp => _clock.Now;

        public string Display => _clock.Now.ToString("HH:mm, d MMMM yyyy");
    }
}