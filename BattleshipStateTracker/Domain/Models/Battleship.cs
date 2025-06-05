using BattleshipStateTracker.Domain.Exceptions;

namespace BattleshipStateTracker.Domain.Models
{
    public class Battleship
    {
        public List<Position> Positions { get; private set; } = new();

        private Battleship() { }

        public static Battleship Create(List<Position> positions)
        {
            if (positions?.Any() != true)
            {
                throw new EmptyBattleshipPositionException();
            }

            var Battleship = new Battleship
            {
                Positions = positions
            };

            return Battleship;
        }

        public void AddPosition(Position position)
        {
            if (position == null)
            {
                throw new PositionNullException();
            }

            Positions.Add(position);
        }

        public string ReceiveAttack(Position position)
        {
            if (position == null)
            {
                Console.WriteLine("Cannot receive a null position attack.");
                throw new PositionNullException("Cannot receive a null position attack.");
            }

            if (Positions.Contains(position))
            {
                //Console.WriteLine("A direct hit!");
                Positions.Remove(position);

                return "A direct hit!";
            }
            else
            {
                //Console.WriteLine("Attack missed.");
                return "Attack missed.";
            }
        }

        public bool HasSunk => Positions.Count() == 0;
    }
}
