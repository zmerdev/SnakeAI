using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeForms.Models
{
    public class Snake
    {
        public Direction? CurrentDirection { get; set; } = Direction.Down;
        public Direction? ChangeTo { get; set; }
        public HashSet<(int X, int Y)> HashCells { get; set; } = new HashSet<(int X, int Y)>();
        public Queue<(int X, int Y)> QueueCells { get; set; } = new Queue<(int X, int Y)>();
        public (int X, int Y) Head { get; private set; }
        public int GrowNum { get; set; } = 3;
        public Dictionary<Direction, (int X, int Y)> DirectionMap { get; } = new Dictionary<Direction, (int X, int Y)>();

        public Snake()
        {
            DirectionMap.Add(Direction.Up, (0, -1));
            DirectionMap.Add(Direction.Down, (0, 1));
            DirectionMap.Add(Direction.Left, (-1, 0));
            DirectionMap.Add(Direction.Right, (1, 0));
            QueueCells.Enqueue((4, 4));
            Head = (4, 4);            
        }

        public void Tick()
        {
            ChangeDirections();
            if (GrowNum <= 0)
            {
                var removed = QueueCells.Dequeue();
                HashCells.Remove(removed);
            }
            else
            {
                GrowNum -= 1;
            }
            var dir = DirectionMap[CurrentDirection.GetValueOrDefault()];

            var newHead = (Head.X + dir.X, Head.Y + dir.Y);
            var oldHead = Head;
            Head = newHead;
            QueueCells.Enqueue(newHead);
            //dont put head in hash set since we want to use it for checking collisions with the head and the body
            HashCells.Add(oldHead);
        }

        public void ChangeDirections()
        {
            //change directions if the direction isnt opposite
            if(ChangeTo == Direction.Down && CurrentDirection != Direction.Up
                || ChangeTo == Direction.Up && CurrentDirection != Direction.Down
                || ChangeTo == Direction.Left && CurrentDirection != Direction.Right
                || ChangeTo == Direction.Right && CurrentDirection != Direction.Left)
            {
                CurrentDirection = ChangeTo;
            }
        }
    }
}
