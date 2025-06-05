namespace BattleshipStateTracker.Application.DTOs
{
    public class BattleshipStartingPosition
    {
        public byte StartRow { get; set; }
        public byte EndRow { get; set; }
        public byte StartColumn { get; set; }
        public byte EndColumn { get; set; }
    }
}
