using System;

namespace Challenges
{
    public struct AdjacentNode : IEquatable<AdjacentNode>
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