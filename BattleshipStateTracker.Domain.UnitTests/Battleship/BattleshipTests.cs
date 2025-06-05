using BattleshipStateTracker.Domain.Exceptions;

namespace BattleshipStateTracker.Domain.UnitTests.Battleship
{

    public class BattleshipTests
    {
        [Fact]
        public void CreateBattleship_WithValidPositions_ShouldCreateBattleship()
        {
            // Arrange
            var positions = new List<Models.Position>
            {
                Models.Position.Create(1, 1),
                Models.Position.Create(1, 2)
            };

            // Act
            var battleship = Models.Battleship.Create(positions);

            // Assert
            Assert.NotNull(battleship);
            Assert.Equal(2, battleship.Positions.Count);
        }

        [Fact]
        public void CreateBattleship_WithEmptyList_ShouldThrowException()
        {
            // Arrange
            var positions = new List<Models.Position>();

            // Act & Assert
            Assert.Throws<EmptyBattleshipPositionException>(() => Models.Battleship.Create(positions));
        }

        [Fact]
        public void AddPosition_WithValidPosition_ShouldAddToList()
        {
            // Arrange
            var battleship = Models.Battleship.Create(
                new List<Models.Position> 
                { 
                    Models.Position.Create(1, 1) 
                }
            );
            var newPosition = Models.Position.Create(2, 2);

            // Act
            battleship.AddPosition(newPosition);

            // Assert
            Assert.Contains(newPosition, battleship.Positions);
        }

        [Fact]
        public void AddPosition_WithNull_ShouldThrowException()
        {
            // Arrange
            var battleship = Models.Battleship.Create(
                new List<Models.Position>
                {
                    Models.Position.Create(1, 1)
                }
            );

            // Act & Assert
            Assert.Throws<PositionNullException>(() => battleship.AddPosition(null));
        }

        [Fact]
        public void ReceiveAttack_Hit_ShouldReturnDirectHit_AndRemovePosition()
        {
            // Arrange
            var target = Models.Position.Create(3, 3);
            var battleship = Models.Battleship.Create(
                new List<Models.Position>
                {
                    target
                }
            );

            // Act
            var result = battleship.ReceiveAttack(target);

            // Assert
            Assert.Equal("A direct hit!", result);
            Assert.DoesNotContain(target, battleship.Positions);
            Assert.True(battleship.HasSunk);
        }

        [Fact]
        public void ReceiveAttack_Miss_ShouldReturnMiss()
        {
            // Arrange
            var battleship = Models.Battleship.Create(
                new List<Models.Position> {
                    Models.Position.Create(1, 1)
                }
            );
            var miss = Models.Position.Create(2, 2);

            // Act
            var result = battleship.ReceiveAttack(miss);

            // Assert
            Assert.Equal("Attack missed.", result);
            Assert.False(battleship.HasSunk);
        }

        [Fact]
        public void ReceiveAttack_WithNull_ShouldThrowException()
        {
            // Arrange
            var battleship = Models.Battleship.Create(
                new List<Models.Position> 
                { 
                    Models.Position.Create(1, 1) 
                }
            );

            // Act & Assert
            Assert.Throws<PositionNullException>(() => battleship.ReceiveAttack(null));
        }

        [Fact]
        public void HasSunk_WhenNoPositionsLeft_ShouldReturnTrue()
        {
            // Arrange
            var position = Models.Position.Create(5, 5);
            var battleship = Models.Battleship.Create(
                new List<Models.Position> 
                { 
                    position 
                }
            );

            // Act
            battleship.ReceiveAttack(position);

            // Assert
            Assert.True(battleship.HasSunk);
        }

        [Fact]
        public void HasSunk_WhenPositionsRemain_ShouldReturnFalse()
        {
            // Arrange
            var battleship = Models.Battleship.Create(
                new List<Models.Position> { 
                    Models.Position.Create(1, 1),
                    Models.Position.Create(1, 2)
                }
            );

            // Act
            battleship.ReceiveAttack(Models.Position.Create(1, 1));

            // Assert
            Assert.False(battleship.HasSunk);
        }
    }

}
