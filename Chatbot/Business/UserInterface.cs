namespace Chatbot.Business
{
    public interface IInstructionReader
    {
        string ReadInstruction();
    }

    public interface IInstructionHandler
    {
        State HandleInstruction(string instruction);
    }

    public class UserInterface
    {
        private readonly IInstructionReader _instructionReader;
        private readonly IInstructionHandler _statusInstructionHandler;

        public UserInterface(IInstructionReader instructionReader, IInstructionHandler instructionHandler)
        {
            _instructionReader = instructionReader;
            _statusInstructionHandler = instructionHandler;
        }

        public State ProcessNextInstruction()
        {
            var instruction = _instructionReader.ReadInstruction();
            return _statusInstructionHandler.HandleInstruction(instruction);
        }
    }
}