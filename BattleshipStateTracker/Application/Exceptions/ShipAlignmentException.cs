namespace BattleshipStateTracker.Application.Exceptions
{
    public class ShipAlignmentException : Exception
    {
        public ShipAlignmentException()
            : base("Ships must be aligned horizontally or vertically.")
        {
        }

        public ShipAlignmentException(string message)
            : base(message)
        {
        }

        public ShipAlignmentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
