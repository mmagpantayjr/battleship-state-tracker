using BattleshipStateTracker.Application.DTOs;
using BattleshipStateTracker.Application.Exceptions;
using BattleshipStateTracker.Domain.Enums;
using BattleshipStateTracker.Domain.Exceptions;
using BattleshipStateTracker.Domain.Models;

namespace BattleshipStateTracker.Application.Services
{
    public class BoardService : IBoardService
    {
        private const byte MINIMUM_ROW_COLUMN_VALUE = 1;
        private const byte MAXIMUM_ROW_COLUMN_VALUE = 10;

        private Battleship _battleship = null!;
        private HashSet<Position> _validPositions = new();
        private HashSet<Position> _attackedPositions = new();

        public void CreateBattleship()
        {
            var startingPosition = SetupStartingPosition();

            var startPosition = Position.Create(startingPosition.StartRow, startingPosition.StartColumn);
            var endPosition = Position.Create(startingPosition.EndRow, startingPosition.EndColumn);

            if (!IsValidPosition(startPosition) || !IsValidPosition(endPosition))
            {
                Console.WriteLine($"[{nameof(BoardService)}] {nameof(CreateBattleship)}: The provided position based on board size is invalid.");
                throw new InvalidBoardSizeException();
            }

            List<Position> positions = new();

            SetupBattleshipPositions(startPosition, endPosition, positions);

            ValidatePositionOverlap(positions);

            var battleship = Battleship.Create(positions);

            _battleship = battleship;
        }

        public void StartBattle()
        {
            while (!HasLost())
            {
                Console.Write("Enter attack position (row and column between 1 and 10, e.g., 1 5): ");
                var input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Invalid input. Please enter row and column.");
                    continue;
                }

                byte row, column;

                if (!TryParsePosition(input, out row, out column))
                {
                    Console.WriteLine("Invalid format. Enter two numbers like: 1 5");
                    continue;
                }

                if (!IsRowColumnValid(row, column))
                {
                    Console.WriteLine("Row and column must be between 1 and 10.");
                    continue;
                }

                try
                {
                    var result = ReceiveAttack(Position.Create(row, column));
                    Console.WriteLine(result);
                }
                catch (InvalidPositionException)
                {
                    Console.WriteLine("That position is invalid. Try again.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"Game over! You Win!");
        }

        private static BattleshipStartingPosition SetupStartingPosition()
        {
            var startingPosition = new BattleshipStartingPosition();

            var random = new Random();
            bool isHorizontal = random.Next(2) == (int)ShipOrientation.Horizontal;

            if (isHorizontal)
            {
                startingPosition.StartRow = (byte)random.Next(1, 11); // Row 1–10
                startingPosition.StartColumn = (byte)random.Next(1, 10); // Column 1–9 to ensure room for at least 2 cells
                byte length = (byte)random.Next(2, 11 - startingPosition.StartColumn + 1); // Length between 2 and max possible
                startingPosition.EndRow = startingPosition.StartRow;
                startingPosition.EndColumn = (byte)(startingPosition.StartColumn + length - 1);
            }
            else
            {
                startingPosition.StartColumn = (byte)random.Next(1, 11); // Column 1–10
                startingPosition.StartRow = (byte)random.Next(1, 10); // Row 1–9
                byte length = (byte)random.Next(2, 11 - startingPosition.StartRow + 1); // Length between 2 and max possible
                startingPosition.EndColumn = startingPosition.StartColumn;
                startingPosition.EndRow = (byte)(startingPosition.StartRow + length - 1);
            }

            return startingPosition;
        }

        private bool HasLost()
        {
            var hasLost = _battleship?.HasSunk ?? false;

            return hasLost;
        }

        private string ReceiveAttack(Position position)
        {
            if (!IsValidPosition(position))
            {
                Console.WriteLine($"[{nameof(BoardService)}] {nameof(ReceiveAttack)}: The provided attack position is invalid.");
                return "";
                //throw new InvalidReceiveAttackPositionException();
            }

            if (_attackedPositions.Contains(position))
            {
                Console.WriteLine($"[{nameof(BoardService)}] {nameof(ReceiveAttack)}: This position has already been attacked.");
                return "";
                //throw new AlreadyAttackedException();
            }

            Console.WriteLine($"[{nameof(BoardService)}] {nameof(ReceiveAttack)}: Attack received at position ({position.Row}, {position.Column}).");

            var attackResult = _battleship.ReceiveAttack(position);
            _attackedPositions.Add(position);


            return attackResult;
        }

        private bool IsRowColumnValid(int row, int column)
        {
            return row is >= MINIMUM_ROW_COLUMN_VALUE and <= MAXIMUM_ROW_COLUMN_VALUE && column is >= MINIMUM_ROW_COLUMN_VALUE and <= MAXIMUM_ROW_COLUMN_VALUE;
        }

        private bool TryParsePosition(string input, out byte row, out byte column)
        {
            row = 0;
            column = 0;

            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
                return false;

            return byte.TryParse(parts[0], out row) && byte.TryParse(parts[1], out column);
        }

        private bool IsValidPosition(Position position) =>
           position.Row > 0 && position.Row <= MAXIMUM_ROW_COLUMN_VALUE &&
                   position.Column > 0 && position.Column <= MAXIMUM_ROW_COLUMN_VALUE;

        private void ValidatePositionOverlap(List<Position> positions)
        {
            foreach (var position in positions)
            {
                if (_validPositions.Any(pos => pos == position))
                {
                    Console.WriteLine($"[{nameof(BoardService)}] {nameof(ValidatePositionOverlap)}: The provided position based on board size is invalid.");
                    //throw new ShipOverlapException();
                }

                _validPositions.Add(position);
            }
        }

        private static void SetupBattleshipPositions(Position startPosition, Position endPosition, List<Position> positions)
        {
            if (startPosition.Row == endPosition.Row) // horizontal
            {
                for (byte col = Math.Min(startPosition.Column, endPosition.Column); col <= Math.Max(startPosition.Column, endPosition.Column); col++)
                {
                    positions.Add(Position.Create(startPosition.Row, col));
                }
            }
            else if (startPosition.Column == endPosition.Column) // vertical
            {
                for (byte row = Math.Min(startPosition.Row, endPosition.Row); row <= Math.Max(startPosition.Row, endPosition.Row); row++)
                {
                    positions.Add(Position.Create(row, startPosition.Column));
                }
            }
            else
            {
                Console.WriteLine($"[{nameof(BoardService)}] {nameof(SetupBattleshipPositions)}: Ships must be aligned horizontally or vertically.");
                //throw new ShipAlignmentException();
            }
        }
    }
}
