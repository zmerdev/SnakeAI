using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeForms.Models
{
    public class Game
    {
        public int Width { get; set; } = 30;
        public int Height { get; set; } = 30;
        public Snake Snake { get; set; } = new Snake();
        public (int X, int Y) Apple { get; set; }
        public bool GameOver { get; set; } = false;
        public bool Win { get; set; } = false;
        private Random Random { get; } = new Random();
        private AStar _ai = new AStar();
        private bool _aiActive = true;
        public List<(int X, int Y)> AiPath = new List<(int X, int Y)>();

        public Game(bool ai = false)
        {
            _aiActive = ai;
            PlaceApple();
        }



        private (int X, int Y) RandomPosition()
        {
            return (Random.Next(0, Width - 1), Random.Next(0, Width - 1));
        }

        private bool IsSpaceEmpty(int X, int Y)
        {
            if (Snake.HashCells.Contains((X, Y)))
            {
                return false;
            }

            if (Snake.Head == (X, Y))
            {
                return false;
            }

            if ((X < 0 || Y < 0 || Y >= Height || X >= Width))
            {
                return false;
            }
            return true;
        }

        private void PlaceApple()
        {
            Apple = RandomPosition();
            while (!IsSpaceEmpty(Apple.X, Apple.Y))
            {
                Apple = RandomPosition();
            }
        }

        private bool CheckSnakeEat()
        {
            if (Snake.Head == Apple)
            {
                return true;
            }
            return false;
        }

        private bool CheckLose()
        {
            var cells = Snake.HashCells;
            if (cells.Contains(Snake.Head))
            {
                return true;
            }
            var head = Snake.Head;
            if (head.X < 0 || head.Y < 0 || head.X >= Width || head.Y >= Height)
            {
                return true;
            }
            return false;
        }

        private bool CheckWin()
        {
            var num_cells = Snake.HashCells.Count;
            if (num_cells == Width * Height - 1)
                return true;
            return false;
        }

        public void Tick()
        {
            if (_aiActive && (AiPath?.Count ?? 0) == 0)
            {
                AiPath = _ai.Solve(Snake.Head, Apple, Snake.HashCells, Height, Width);
            }

            if ((AiPath?.Count?? 0) > 0)
            {
                var nextPos = AiPath.Last();
                var x = nextPos.X - Snake.Head.X;
                var y = nextPos.Y - Snake.Head.Y;
                var key = Snake.DirectionMap.FirstOrDefault(v => v.Value == (x, y)).Key;
                Snake.ChangeTo = key;
                AiPath.RemoveAt(AiPath.Count - 1);
            }

            Snake.Tick();
            if (CheckWin())
            {
                Win = true;
                GameOver = true;
                return;
            }

            if (CheckLose())
            {
                GameOver = true;
                return;
            }

            if (CheckSnakeEat())
            {
                Snake.GrowNum += 1;
                PlaceApple();
            }
        }
    }
}
