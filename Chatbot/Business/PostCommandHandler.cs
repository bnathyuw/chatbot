using System.Text.RegularExpressions;

namespace Chatbot.Business
{
    public interface IMessageSaver
    {
        void SaveMessage(Message message);
    }

    public class PostCommandHandler : ICommandHandler
    {
        private readonly IClock _clock;
        private readonly IMessageSaver _messageSaver;
        private readonly ICommandHandler _successor;
        private readonly Regex _regex = new Regex("^(?<user>[a-zA-Z]*) -> (?<text>.*)$");

        public PostCommandHandler(ICommandHandler successor, IClock clock, IMessageSaver messageSaver)
        {
            _clock = clock;
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
                SentOn = _clock.Now
            };
        }
    }
}