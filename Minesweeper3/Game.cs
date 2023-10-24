using System;

namespace Minesweeper3
{
    public class Game
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public DateTime? Start { get; private set; }
        public DateTime? End { get; private set; }
        public GameState State { get; private set; }

        public static Game New(int width, int heigth)
        {
            return new Game()
            {
                Width = width,
                Height = heigth,
            };
        }
    }

    public enum GameState
    {
        Playing, Win, Lose
    }
}
