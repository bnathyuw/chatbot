using System;

namespace Chatbot.Tests.Specs
{
    public static class TimespanCreator
    {
        public static TimeSpan MinutesAgo(this int minutes) => TimeSpan.FromMinutes(-minutes);
        public static TimeSpan MinuteAgo(this int minutes) => MinutesAgo(minutes);
    }
}