using System;

namespace Chatbot.Business
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}