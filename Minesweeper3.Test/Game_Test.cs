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
            game.StartTime.Should().BeNull();
            game.EndTime.Should().BeNull();
            game.State.Should().Be(GameState.Playing);
            game.Cells.Count.Should().Be(2);
        }

        [TestMethod]
        public void _02_Cells()
        {
            Game game = Game.New(2, 1);
            game.Cells.Count.Should().Be(2);

            game.Cells[(0, 0)].State.Should().Be(CellState.Default);
            game.Cells[(0, 0)].IsMine.Should().BeFalse();
        }

        [TestMethod]
        public void _03_SetMine()
        {
            Game game = Game.New(2, 1);
            
            game.Cells[(0, 0)].SetMine();
            game.Cells[(0, 0)].IsMine.Should().BeTrue();
            game.Cells[(1, 0)].IsMine.Should().BeFalse();
        }

        [TestMethod]
        public void _04_SetMine_Game()
        {
            Game game = Game.New(2, 1)
                .SetMine(0, 0);

            game.Cells[(0, 0)].IsMine.Should().BeTrue();
            game.Cells[(1, 0)].IsMine.Should().BeFalse();
        }

        [TestMethod]
        public void _05_RandomizeMines()
        {
            Game game = Game.New(10, 10)
                .RandomizeMines(10);

            game.Cells.Values.Where(c => c.IsMine == true).Count().Should().Be(10);
            game.Cells.Values.Where(c => c.IsMine == false).Count().Should().Be(90);
        }

        [TestMethod]
        public void _06_Start()
        {
            DateTime now = DateTime.Now;
            Game game = Game.New(2, 1)
                .SetMine(0, 0)
                .Start();

            game.StartTime.Should().NotBeNull();
            game.StartTime.Should().BeOnOrAfter(now);
        }

        [TestMethod]
        public void _07_Pick()
        {
            Game game = Game.New(2, 1)
                .SetMine(0, 0)
                .Start();

            game.Cells[(1, 0)].IsPicked.Should().BeFalse();
            game.Cells[(1, 0)].Pick();
            game.Cells[(1, 0)].IsPicked.Should().BeTrue();
        }

        [TestMethod]
        public void _08_Pick_Game()
        {
            Game game = Game.New(2, 1)
                .SetMine(0, 0)
                .Start();

            game.Cells[(1, 0)].IsPicked.Should().BeFalse();
            game.Pick(1, 0);
            game.Cells[(1, 0)].IsPicked.Should().BeTrue();
        }

        [TestMethod]
        public void _09_Lose()
        {
            Game game = Game.New(2, 1)
                .SetMine(0, 0)
                .Start();

            game.State.Should().Be(GameState.Playing);
            game.Pick(0, 0);
            game.State.Should().Be(GameState.Lose);
        }
    }
}