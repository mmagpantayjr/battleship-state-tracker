using BattleshipStateTracker.Application.UnitTests.BoardService.Fixtures;

namespace BattleshipStateTracker.Application.UnitTests.BoardService
{

    public abstract class BoardServiceTestBase : IClassFixture<BoardServiceFixture>
    {
        protected readonly Services.BoardService _boardService;

        protected BoardServiceTestBase(BoardServiceFixture fixture)
        {
            _boardService = fixture.BoardService;
        }
    }
}
