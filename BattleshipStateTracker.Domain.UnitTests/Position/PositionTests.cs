using BattleshipStateTracker.Domain.Exceptions;

namespace BattleshipStateTracker.Domain.UnitTests.Position
{

    public class PositionTests
    {
        [Fact]
        public void CreatePosition_WithValidRowAndColumn_ShouldReturnPosition()
        {
            // Arrange
            byte row = 5;
            byte column = 7;

            // Act
            var position = Models.Position.Create(row, column);

            // Assert
            Assert.NotNull(position);
            Assert.Equal(row, position.Row);
            Assert.Equal(column, position.Column);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(0, 0)]
        public void CreatePosition_WithZeroRowOrColumn_ShouldThrowInvalidPositionException(byte row, byte column)
        {
            // Act & Assert
            Assert.Throws<InvalidPositionException>(() => Models.Position.Create(row, column));
        }

        [Fact]
        public void Equals_WithSameRowAndColumn_ShouldReturnTrue()
        {
            // Arrange
            var p1 = Models.Position.Create(2, 3);
            var p2 = Models.Position.Create(2, 3);

            // Act & Assert
            Assert.Equal(p1, p2);
            Assert.True(p1.Equals(p2));
        }

        [Fact]
        public void Equals_WithDifferentRowOrColumn_ShouldReturnFalse()
        {
            // Arrange
            var p1 = Models.Position.Create(2, 3);
            var p2 = Models.Position.Create(3, 2);

            // Act & Assert
            Assert.NotEqual(p1, p2);
            Assert.False(p1.Equals(p2));
        }
    }

}
