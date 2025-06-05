namespace BattleshipStateTracker.Application.Exceptions
{
    public class InvalidReceiveAttackPositionException : Exception
    {
        public InvalidReceiveAttackPositionException()
            : base("The provided attack position is invalid.")
        {
        }

        public InvalidReceiveAttackPositionException(string message)
            : base(message)
        {
        }

        public InvalidReceiveAttackPositionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
