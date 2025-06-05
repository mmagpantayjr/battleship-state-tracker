namespace BattleshipStateTracker.Domain.Exceptions
{
    public class InvalidPositionException : Exception
    {
        public InvalidPositionException()
            : base("Positions row or column cannot be 0.")
        {
        }

        public InvalidPositionException(string message)
            : base(message)
        {
        }

        public InvalidPositionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
