using System.Text.RegularExpressions;

namespace Chatbot.Business
{
    public class PostInstructionHandler : IInstructionHandler
    {
        private readonly IClock _clock;
        private readonly IMessageSaver _messageSaver;

        public PostInstructionHandler(IClock clock, IMessageSaver messageSaver)
        {
            _clock = clock;
            _messageSaver = messageSaver;
        }

        public State HandleInstruction(string instruction)
        {
            var regex = new Regex("^(?<user>[a-zA-Z]*) -> (?<text>.*)$");

            var match = regex.Match(instruction);
            var user = match.Groups["user"].Value;
            var text = match.Groups["text"].Value;
            var sentOn = _clock.Now;
            var message = new Message {User = user, Text = text, SentOn = sentOn};
            _messageSaver.SaveMessage(message);
            return State.Continue;
        }
    }

    public interface IMessageSaver
    {
        void SaveMessage(Message message);
    }
}