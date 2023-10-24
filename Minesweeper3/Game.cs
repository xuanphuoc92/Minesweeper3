﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Minesweeper3
{
    public class Game
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
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

        public Game SetMine(int x, int y)
        {
            Cells[(x, y)].SetMine();
            return this;
        }

        public Game RandomizeMines(int mines)
        {
            Random rnd = new Random();
            Cells
                .Values // Take all Cells
                .OrderBy(c => rnd.Next()) // Order randomly
                .Take(mines) // Take first {mines} Cells
                .ToList().ForEach(c => c.SetMine()); // Set as Mines
            return this;
        }

        public Game Start()
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    CalculateNumber(x, y);

            StartTime = DateTime.Now;
            return this;
        }

        private void CalculateNumber(int x, int y)
        {
            Cell cell = Cells[(x, y)];
            if (cell.IsMine) return;
            List<Cell> aroundCells = GetAroundCells(x, y);
            cell.SetNumber(aroundCells.Count(c => c.IsMine));
        }

        private List<Cell> GetAroundCells(int x, int y)
        {
            List<Cell> results = new List<Cell>();
            for (int x2 = x - 1; x2 <= x + 1; x2++)
                for (int y2 = y - 1; y2 <= y + 1; y2++)
                {
                    if (x2 == x && y2 == y) continue;
                    if (Cells.ContainsKey((x2, y2)) == false) continue;
                    results.Add(Cells[(x2, y2)]);
                }
            return results;
        }

        public Game Pick(int x, int y)
        {
            if (State != GameState.Playing)
                return this;

            Cell cell = Cells[(x, y)];
            cell.Pick();
            if (cell.IsMine == true)
                Lose();
            CheckWin();
            return this;
        }

        private void CheckWin()
        {
            if (Cells.Values
                .Where(c => c.IsMine == false)
                .All(c => c.IsPicked == true))
            {
                State = GameState.Win;
                EndTime = DateTime.Now;
            }
        }

        private void Lose()
        {
            State = GameState.Lose;
            EndTime = DateTime.Now;
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
        public bool IsPicked { get; private set; }
        public int Number { get; private set; }

        public void Pick()
        {
            IsPicked = true;
        }

        public void SetMine()
        {
            IsMine = true;
        }

        public void SetNumber(int number)
        {
            Number = number;
        }
    }

    public enum CellState
    {
        Default, Picked, Flagged
    }
}
