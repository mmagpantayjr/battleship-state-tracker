using BattleshipStateTracker.Domain.Exceptions;

namespace BattleshipStateTracker.Domain.Models
{
    public class Position
    {
        private Position() { }
        public byte Row { get; private set; }
        public byte Column { get; private set; }

        public static Position Create(byte row, byte column)
        {
            if (row == default || column == default)
            {
                Console.WriteLine($"[{nameof(Position)}] {nameof(Create)}: Positions row or column cannot be 0.");
                throw new InvalidPositionException();
            }

            var position = new Position
            {
                Row = row,
                Column = column
            };

            return position;
        }

        public override bool Equals(object? obj)
        {
            return obj is Position other &&
                   Row == other.Row &&
                   Column == other.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }
    }
}
