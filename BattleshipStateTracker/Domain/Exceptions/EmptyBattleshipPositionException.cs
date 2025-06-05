namespace BattleshipStateTracker.Domain.Exceptions
{
    public class EmptyBattleshipPositionException : Exception
    {
        public EmptyBattleshipPositionException()
            : base("Positions list cannot be null or empty when creating a Battleship.")
        {
        }

        public EmptyBattleshipPositionException(string message)
            : base(message)
        {
        }

        public EmptyBattleshipPositionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
