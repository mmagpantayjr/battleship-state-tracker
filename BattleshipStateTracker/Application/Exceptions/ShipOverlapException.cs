namespace BattleshipStateTracker.Application.Exceptions
{
    public class ShipOverlapException : Exception
    {
        public ShipOverlapException()
            : base("The provided position based on board size is invalid.")
        {
        }

        public ShipOverlapException(string message)
            : base(message)
        {
        }

        public ShipOverlapException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
