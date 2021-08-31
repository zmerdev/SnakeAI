using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeForms
{
    public class AStar
    {


        public List<(int X, int Y)> Solve((int X, int Y) start, (int X, int Y) end, HashSet<(int, int)> obstacles, int h, int w)
        {
            List<Node> OpenList = new List<Node>();
            List<Node> ClosedList = new List<Node>();

            OpenList.Add(new Node(start.X, start.Y));
            OpenList.Sort();
            while (OpenList.Count > 0)
            {
                var currentNode = OpenList[0];
                OpenList.RemoveAt(0);
                ClosedList.Add(currentNode);

                if (currentNode.Position == end)
                {
                    var nodeIter = currentNode;
                    var list = new List<(int X, int Y)>();
                    while (nodeIter != null)
                    {
                        if (nodeIter.Parent != null)
                        {
                            list.Add(nodeIter.Position);
                        }
                        nodeIter = nodeIter.Parent;
                    }

                    return list;
                }
                (int X, int Y)[] directions = { (0, 1), (0, -1), (1, 0), (-1, 0) };

                foreach (var dir in directions)
                {
                    var x = dir.X + currentNode.Position.X;
                    var y = dir.Y + currentNode.Position.Y;

                    //node imposible
                    if (x < 0 || x >= w || y < 0 || y >= h || obstacles.Contains((x, y)))
                    {
                        continue;
                    }

                    //node in closed already
                    if (ClosedList.Where(n => n.Position.X == x && n.Position.Y == y).Count() > 0)
                    {
                        continue;
                    }

                    var node = new Node(x, y);
                    node.Parent = currentNode;
                    node.G = currentNode.G + 1;
                    node.H = Math.Abs(end.X - x) + Math.Abs(end.Y - y);

                    //node is in open list already
                    var matchOpen = OpenList.Where(n => n.Position == node.Position);
                    if (matchOpen.Count() > 0)
                    {
                        var oldnode = matchOpen.First();
                        if (oldnode.G > node.G)
                        {
                            oldnode.Parent = currentNode;
                            oldnode.G = node.G;
                        }
                        continue;
                    }

                    OpenList.Add(node);
                }


            }

            return null;
        }
    }

    public class Node : IComparable<Node>
    {
        public Node Parent;
        public (int X, int Y) Position;
        //g+h
        public int F { get { return G + H; } }
        //cost so far from start
        public int G;
        //hueristic cost to end
        public int H;
        public Node(int x, int y)
        {
            Position = (x, y);
        }

        public int CompareTo(Node obj)
        {
            return -1 * (F - obj.F);
        }
    }


}
