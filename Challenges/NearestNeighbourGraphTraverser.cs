using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenges
{
    public class NearestNeighbourGraphTraverser
    {
        private readonly IGraph _graph;
        private readonly ISet<int> _intersections;

        public NearestNeighbourGraphTraverser(IGraph graph, ISet<int> intersections)
        {
            _graph = graph;
            _intersections = intersections;
        }

        public IList<int> GetNearestNeighbours(int nodeIndex)
        {
            var visited = new bool[_graph.GetNumberOfVerticies()];
            visited[nodeIndex] = true;
            var nearestNeighbours = new List<int>();
            foreach (var adjacentVertex in _graph.GetAdjacentVerticies(nodeIndex))
            {
                var nearestIntersection = GetNearestIntersection(adjacentVertex, visited, _intersections.Contains);
                if (nearestIntersection.HasValue)
                {
                    nearestNeighbours.Add(nearestIntersection.Value);
                }
            }
            return nearestNeighbours;
        }

        private int? GetNearestIntersection(int index, bool[] visited, Func<int, bool> shortCircuit)
        {
            var bfsQueue = new Queue<int>();
            bfsQueue.Enqueue(index);

            do
            {
                var currentIndex = bfsQueue.Dequeue();
                visited[currentIndex] = true;

                if (shortCircuit(currentIndex))
                {
                    return currentIndex;
                }
                var adjacentVerticies = _graph.GetAdjacentVerticies(currentIndex);
                foreach (var vertex in adjacentVerticies)
                {
                    if (!visited[vertex])
                    {
                        bfsQueue.Enqueue(vertex);
                    }
                }
            } while (bfsQueue.Any());

            return null;
        }
    }
}
