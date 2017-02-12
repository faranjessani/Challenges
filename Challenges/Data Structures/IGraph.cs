using System.Collections.Generic;

namespace Challenges
{
    public interface IGraph
    {
        void AddEdge(int vertex1, int vertex2);
        IEnumerable<int> GetAdjacentVerticies(int vertex);
        int GetNumberOfVerticies();
    }
}