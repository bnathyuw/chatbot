using System;
using System.Text.RegularExpressions;

namespace Chatbot.Business
{
    public interface IMessageSaver
    {
        void SaveMessage(Message message);
    }

    public interface ITimestamper
    {
        DateTime Timestamp { get; }
    }

    public class PostCommandHandler : ICommandHandler
    {
        private readonly IMessageSaver _messageSaver;
        private readonly ICommandHandler _successor;
        private readonly Regex _regex = new Regex("^(?<user>[a-zA-Z]*) -> (?<text>.*)$");
        private readonly ITimestamper _timestamper;

        public PostCommandHandler(ICommandHandler successor, IMessageSaver messageSaver, ITimestamper timestamper)
        {
            _timestamper = timestamper;
            _messageSaver = messageSaver;
            _successor = successor;
        }

        public State Handle(string command)
        {
            var match = _regex.Match(command);

            if (!match.Success)
                return _successor.Handle(command);

            var message = ParseMessage(match);
            _messageSaver.SaveMessage(message);
            return State.Continue;
        }

        private Message ParseMessage(Match match)
        {
            return new Message
            {
                User = match.Groups["user"].Value,
                Text = match.Groups["text"].Value,
                SentOn = _timestamper.Timestamp
            };
        }
    }
}