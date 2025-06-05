namespace BattleshipStateTracker.Domain.Exceptions
{
    public class PositionNullException : Exception
    {
        public PositionNullException()
            : base("A null position cannot be added to the Battleship.")
        {
        }

        public PositionNullException(string message)
            : base(message)
        {
        }

        public PositionNullException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
