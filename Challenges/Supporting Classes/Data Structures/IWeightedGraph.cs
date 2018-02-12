using System.Collections.Generic;

namespace Challenges
{
    public interface IWeightedGraph : IGraph
    {
        IEnumerable<AdjacentNode> GetAdjacentNodes(int vertex);
    }
}