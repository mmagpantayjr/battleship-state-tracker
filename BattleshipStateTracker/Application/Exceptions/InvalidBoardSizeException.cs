namespace BattleshipStateTracker.Application.Exceptions
{
    public class InvalidBoardSizeException : Exception
    {
        public InvalidBoardSizeException()
            : base("The provided position based on board size is invalid.")
        {
        }

        public InvalidBoardSizeException(string message)
            : base(message)
        {
        }

        public InvalidBoardSizeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
