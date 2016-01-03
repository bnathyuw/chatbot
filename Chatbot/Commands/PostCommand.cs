using System;
using System.Text.RegularExpressions;
using Chatbot.Control;

namespace Chatbot.Commands
{
    public interface IMessageSaver
    {
        void SaveMessage(Message message);
    }

    public interface ITimestamper
    {
        DateTime Timestamp { get; }
    }

    public class PostCommand : ICommand
    {
        private readonly Regex _regex = new Regex("^(?<user>[a-zA-Z]*) -> (?<text>.*)$");

        private readonly IMessageSaver _messageSaver;
        private readonly ITimestamper _timestamper;

        public PostCommand(ITimestamper timestamper, IMessageSaver messageSaver)
        {
            _timestamper = timestamper;
            _messageSaver = messageSaver;
        }

        public State Do(string command)
        {
            var message = ParseMessage(command);
            _messageSaver.SaveMessage(message);
            return State.Continue;
        }

        private Message ParseMessage(string command)
        {
            var match = _regex.Match(command);
            return new Message
            {
                User = match.Groups["user"].Value,
                Text = match.Groups["text"].Value,
                SentOn = _timestamper.Timestamp
            };
        }

        public bool Matches(string command) => _regex.IsMatch(command);
    }
}