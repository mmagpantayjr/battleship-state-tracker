namespace BattleshipStateTracker.Application.Exceptions
{
    public class AlreadyAttackedException : Exception
    {
        public AlreadyAttackedException()
            : base("This position has already been attacked.")
        {
        }

        public AlreadyAttackedException(string message)
            : base(message)
        {
        }

        public AlreadyAttackedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
