using FluentAssertions;

namespace Minesweeper3.Test
{
    [TestClass]
    public class Game_Test
    {
        [TestMethod]
        public void _01_NewGame()
        {
            Game game = Game.New(2, 1);

            game.Width.Should().Be(2);
            game.Height.Should().Be(1);
            game.Start.Should().BeNull();
            game.End.Should().BeNull();
            game.State.Should().Be(GameState.Playing);
        }
    }
}