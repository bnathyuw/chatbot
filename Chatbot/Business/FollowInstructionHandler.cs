using System.Text.RegularExpressions;

namespace Chatbot.Business
{
    public interface IUserConnexionSaver
    {
        void SaveConnexion(string follower, string followed);
    }

    public class FollowInstructionHandler : IInstructionHandler
    {
        private readonly IInstructionHandler _successor;
        private readonly IUserConnexionSaver _userConnexionSaver;
        private readonly Regex _regex = new Regex("^(?<follower>[a-zA-Z]*) follows (?<followed>[a-zA-Z]*)$");

        public FollowInstructionHandler(IInstructionHandler successor, IUserConnexionSaver userConnexionSaver)
        {
            _successor = successor;
            _userConnexionSaver = userConnexionSaver;
        }

        public State HandleInstruction(string instruction)
        {
            var match = _regex.Match(instruction);

            if(!match.Success)
                return _successor.HandleInstruction(instruction);

            var follower = match.Groups["follower"].Value;
            var followed = match.Groups["followed"].Value;

            _userConnexionSaver.SaveConnexion(follower, followed);

            return State.Continue;
        }
    }
}