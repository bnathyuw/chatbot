using System.Text.RegularExpressions;

namespace Chatbot.Business
{
    public interface IMessageSaver
    {
        void SaveMessage(Message message);
    }

    public class PostInstructionHandler : IInstructionHandler
    {
        private readonly IClock _clock;
        private readonly IMessageSaver _messageSaver;
        private readonly IInstructionHandler _successor;
        private readonly Regex _regex = new Regex("^(?<user>[a-zA-Z]*) -> (?<text>.*)$");

        public PostInstructionHandler(IClock clock, IMessageSaver messageSaver, IInstructionHandler successor)
        {
            _clock = clock;
            _messageSaver = messageSaver;
            _successor = successor;
        }

        public State HandleInstruction(string instruction)
        {
            var match = _regex.Match(instruction);

            if (!match.Success)
                return _successor.HandleInstruction(instruction);

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