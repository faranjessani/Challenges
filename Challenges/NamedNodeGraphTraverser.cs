using System.Collections.Generic;
using System.Linq;

namespace Challenges
{
    public class NamedNodeGraphTraverser
    {
        private readonly IGraph _graph;
        public NamedNodeGraphTraverser(IGraph graph)
        {
            _graph = graph;
        }

        public int GetPathLength(int startingNode, int endingNode)
        {
            var visited = new bool[_graph.GetNumberOfVerticies()];
            var edgeTo = new int[_graph.GetNumberOfVerticies()];

            int node = 0;
            var bfsQueue = new Queue<int>();
            bfsQueue.Enqueue(startingNode);
            do
            {
                node = bfsQueue.Dequeue();
                if (node == endingNode) break;
                if (visited[node]) continue;
                visited[node] = true;

                foreach (var adjacentVerticy in _graph.GetAdjacentVerticies(node))
                {
                    if (visited[adjacentVerticy]) continue;

                    edgeTo[adjacentVerticy] = node;
                    bfsQueue.Enqueue(adjacentVerticy);
                }

            } while (bfsQueue.Any());

            node = edgeTo[endingNode];
            int countOfHops = 1;
            do
            {
                node = edgeTo[node];
                countOfHops++;
            } while (node != startingNode);

            return countOfHops;
        }
    }
}