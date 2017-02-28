using System.Collections.Generic;
using System.Linq;

namespace Challenges
{
    public class WeightedAdjacencyListGraph : IWeightedGraph
    {
        private readonly HashSet<AdjacentNode>[] _nodes;

        public WeightedAdjacencyListGraph(int size)
        {
            _nodes = new HashSet<AdjacentNode>[size];
            for (int i = 0; i < _nodes.Length; i++)
            {
                _nodes[i] = new HashSet<AdjacentNode>();
            }
        }

        public IEnumerable<int> GetAdjacentVerticies(int vertex)
        {
            return _nodes[vertex].Select(n => n.NodeId);
        }

        public IEnumerable<AdjacentNode> GetAdjacentNodes(int vertex)
        {
            return _nodes[vertex];
        }

        public int GetNumberOfVerticies()
        {
            return _nodes.Length;
        }

        public void AddEdge(int vertex1, int vertex2, int weight)
        {
            _nodes[vertex1].Add(new AdjacentNode(vertex2, weight));
            _nodes[vertex2].Add(new AdjacentNode(vertex1, weight));
        }
    }
}
