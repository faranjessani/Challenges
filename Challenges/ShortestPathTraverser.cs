using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenges
{
    public class ShortestPathTraverser
    {
        private readonly IWeightedGraph _graph;
        private readonly Maze _maze;

        public ShortestPathTraverser(IWeightedGraph graph, Maze maze)
        {
            _graph = graph;
            _maze = maze;
        }

        public IEnumerable<Edge> TraverseFullGraphFrom(int startingNode)
        {
            var path = new Queue<Edge>(_graph.GetNumberOfVerticies() - 1);
            var visited = new bool[_graph.GetNumberOfVerticies()];
            visited[startingNode] = true;

            do
            {
                var adjacentNodes = _graph.GetAdjacentNodes(startingNode);
                var minNode = adjacentNodes.Where(n => !visited[n.NodeId])
                    .OrderBy(n => HeuristicFunction(startingNode, n))
                    .First();
                visited[minNode.NodeId] = true;
                path.Enqueue(new Edge(minNode.Weight, startingNode, minNode.NodeId));
                startingNode = minNode.NodeId;
            } while (visited.Any(v => !v));

            return path;
        }

        private double HeuristicFunction(int startingNode, AdjacentNode node)
        {
            var nodeMazeIndex = _maze.GetMazeIndexForNamedNode((char) ('0' + node.NodeId));
            var startingNodeMazeIndex = _maze.GetMazeIndexForNamedNode((char) ('0' + 7));

            var heuristicValue =
                Math.Sqrt(Math.Pow(Math.Abs(nodeMazeIndex.Item1 - startingNodeMazeIndex.Item1), 2) +
                          Math.Pow(Math.Abs(nodeMazeIndex.Item2 - startingNodeMazeIndex.Item2), 2));
            var weight = node.Weight;

            return heuristicValue + weight;
        }
    }

    public struct Edge
    {
        public override string ToString()
        {
            return $"{nameof(Weight)}: {Weight}, {nameof(StartingNode)}: {StartingNode}, {nameof(EndingNode)}: {EndingNode}";
        }

        public int Weight { get; }
        public int StartingNode { get; }
        public int EndingNode { get; }

        public Edge(int weight, int startingNode, int endingNode)
        {
            Weight = weight;
            StartingNode = startingNode;
            EndingNode = endingNode;
        }
    }
}
