using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenges
{
    public class Maze
    {
        private readonly char[,] _maze;
        private readonly int _height;
        private readonly int _width;
        private IDictionary<char, int> _namedNodeToGraphIndexMap;

        public Maze(string maze)
        {
            var mazeAsRows = maze.Split('\n').Select(r => r.Trim()).ToArray();
            _maze = new char[mazeAsRows.Length - 2, mazeAsRows[0].Length - 2];
            for (var i = 1; i < mazeAsRows.Length - 1; i++)
            {
                for (var j = 1; j < mazeAsRows[i].Length - 1; j++)
                {
                    _maze[i - 1, j - 1] = mazeAsRows[i][j];
                }
            }
            _height = _maze.GetLength(0);
            _width = _maze.GetLength(1);
            _namedNodeToGraphIndexMap = new Dictionary<char, int>();
        }

        public int GetNodeCount()
        {
            return _height * _width;
        }

        public int GetGraphNodeIndex(Tuple<int, int> node)
        {
            return node.Item1 * _width + node.Item2;
        }

        public Tuple<int, int> ConvertGraphIndexToMazeIndex(int index)
        {
            int x = index / _width;
            int y = index % _width;

            return new Tuple<int, int>(x, y);
        }

        public void ParseMazeIntoGraph(ref IGraph graph)
        {
            for (var i = 0; i < _height; i++)
            {
                for (var j = 0; j < _width; j++)
                {
                    var currentNode = new Tuple<int, int>(i, j);
                    if (!IsNode(currentNode)) continue;

                    var neighbours = GetNeighboursOf(currentNode);
                    foreach (var neighbour in neighbours)
                    {
                        if (IsNode(neighbour))
                        {
                            graph.AddEdge(GetGraphNodeIndex(currentNode), GetGraphNodeIndex(neighbour));
                        }
                    }
                }
            }
        }

        protected IEnumerable<Tuple<int, int>> GetNeighboursOf(Tuple<int, int> node)
        {
            return new Tuple<int, int>[4]
            {
                new Tuple<int, int>(node.Item1, node.Item2 - 1),
                new Tuple<int, int>(node.Item1, node.Item2 + 1),
                new Tuple<int, int>(node.Item1 - 1, node.Item2),
                new Tuple<int, int>(node.Item1 + 1, node.Item2)
            };
        }

        public bool IsNode(Tuple<int, int> node)
        {
            if (node.Item1 >= _height || node.Item2 >= _width ||
                node.Item1 < 0 || node.Item2 < 0)
                return false;

            var nodeCharacter = _maze[node.Item1, node.Item2];
            if (nodeCharacter == '#')
                return false;
            if (nodeCharacter != '.')
                _namedNodeToGraphIndexMap[nodeCharacter] = GetGraphNodeIndex(node);

            return true;
        }

        public int GetGraphNodeIndexForNamedNode(char namedNode)
        {
            return _namedNodeToGraphIndexMap[namedNode];
        }
    }
}