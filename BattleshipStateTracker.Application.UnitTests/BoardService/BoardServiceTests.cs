using BattleshipStateTracker.Application.UnitTests.BoardService.Fixtures;
using BattleshipStateTracker.Domain.Models;

namespace BattleshipStateTracker.Application.UnitTests.BoardService
{
    public class BoardServiceTests : IClassFixture<BoardServiceFixture>
    {
        private readonly Services.BoardService _boardService;
        private readonly BoardServiceFixture _fixture;

        public BoardServiceTests(BoardServiceFixture fixture)
        {
            _boardService = fixture.BoardService;
            _fixture = fixture;
        }

        [Fact]
        public void CreateBattleship_ShouldInitializeBattleshipSuccessfully()
        {
            _boardService.CreateBattleship();

            var field = typeof(Services.BoardService).GetField("_battleship", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var battleship = field?.GetValue(_boardService);

            Assert.NotNull(battleship);
        }

        [Fact]
        public void ReceiveAttack_ValidHit_ShouldReturnHitMessage()
        {
            var positions = new List<Position>
            {
                Position.Create(2, 3),
                Position.Create(2, 4)
            };

            _fixture.SetupCustomBattleship(positions);

            var result = _fixture.CallReceiveAttack(Position.Create(2, 3));

            Assert.Equal("A direct hit!", result);
        }

        [Fact]
        public void ReceiveAttack_InvalidPosition_ShouldReturnEmptyMessage()
        {
            _fixture.SetupCustomBattleship(new List<Position> { Position.Create(2, 3), Position.Create(2, 4) });

            var result = _fixture.CallReceiveAttack(Position.Create(20, 30));

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ReceiveAttack_RepeatedAttack_ShouldReturnEmptyMessage()
        {
            var position = Position.Create(2, 3);
            _fixture.SetupCustomBattleship(new List<Position> { position, Position.Create(2, 4) });

            _fixture.CallReceiveAttack(position); // First hit
            var result = _fixture.CallReceiveAttack(position); // Repeated hit

            Assert.Equal(string.Empty, result);
        }
    }

}
