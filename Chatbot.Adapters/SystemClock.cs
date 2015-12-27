using System;
using Chatbot.Business;

namespace Chatbot.Adapters
{
    public class SystemClock : IClock
    {
        public DateTime Now => DateTime.Now;
    }
}