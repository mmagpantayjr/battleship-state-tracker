using BattleshipStateTracker.Domain.Models;
using System.Reflection;

namespace BattleshipStateTracker.Application.UnitTests.BoardService.Fixtures
{
    public class BoardServiceFixture
    {
        public Services.BoardService BoardService { get; private set; } = null!;


        public void SetupCustomBattleship(List<Position> positions)
        {
            BoardService = new Services.BoardService();

            var battleship = Battleship.Create(positions);
            typeof(Services.BoardService)
                .GetField("_battleship", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(BoardService, battleship);
        }

        public string CallReceiveAttack(Position position)
        {
            var method = typeof(Services.BoardService).GetMethod("ReceiveAttack", BindingFlags.NonPublic | BindingFlags.Instance);
            return method?.Invoke(BoardService, new object[] { position }) as string ?? string.Empty;
        }
    }
}
