using BattleshipStateTracker.Domain.Models;

namespace BattleshipStateTracker.Application.Services
{
    public interface IBoardService
    {
        void CreateBattleship();
        void StartBattle();
    }
}
