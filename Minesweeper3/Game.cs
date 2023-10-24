using System;
using System.Collections.Generic;

namespace Minesweeper3
{
    public class Game
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public DateTime? Start { get; private set; }
        public DateTime? End { get; private set; }
        public GameState State { get; private set; }

        public Dictionary<(int, int), Cell> Cells { get; private set; }
            = new Dictionary<(int, int), Cell>();

        public static Game New(int width, int heigth)
        {
            Game game = new Game()
            {
                Width = width,
                Height = heigth,
            };

            for (int x = 0; x < width; x++)
                for (int y = 0; y < heigth; y++)
                    game.Cells[(x,y)] = new Cell();

            return game;
        }
    }

    public enum GameState
    {
        Playing, Win, Lose
    }

    public class Cell
    {
        public CellState State { get; private set; }
        public bool IsMine { get; private set; }
    }

    public enum CellState
    {
        Default, Picked, Flagged
    }
}
