using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenges
{

    public class WeightedAdjacencyListGraph : IGraph
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

        public int GetNumberOfVerticies()
        {
            return _nodes.Length;
        }

        public void AddEdge(int vertex1, int vertex2, int weight)
        {
            _nodes[vertex1].Add(new AdjacentNode(vertex2, weight));
            _nodes[vertex2].Add(new AdjacentNode(vertex1, weight));
        }

        internal struct AdjacentNode : IEquatable<AdjacentNode>
        {
            public int NodeId { get; }
            public int Weight { get; }

            public AdjacentNode(int nodeId, int weight)
            {
                NodeId = nodeId;
                Weight = weight;
            }

            public override string ToString()
            {
                return $"{nameof(NodeId)}: {NodeId}, {nameof(Weight)}: {Weight}";
            }

            public bool Equals(AdjacentNode other)
            {
                return NodeId == other.NodeId && Weight == other.Weight;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is AdjacentNode && Equals((AdjacentNode) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (NodeId * 397) ^ Weight;
                }
            }
        }
    }
}
