using System.Collections.Generic;

namespace Challenges
{
    public class AdjacencyMatrixGraph : IGraph
    {
        private readonly int[,] _adjacencyMatrix;

        public AdjacencyMatrixGraph(int size)
        {
            _adjacencyMatrix = new int[size, size];
        }

        public void AddEdge(int vertex1, int vertex2)
        {
            _adjacencyMatrix[vertex1, vertex2]++;
            _adjacencyMatrix[vertex2, vertex1]++;
        }

        public IEnumerable<int> GetAdjacentVerticies(int vertex)
        {
            ISet<int> result = new HashSet<int>();

            for (var i = 0; i < _adjacencyMatrix.GetLength(0); i++)
                if (_adjacencyMatrix[vertex, i] > 0)
                    result.Add(i);

            return result;
        }

        public int GetNumberOfVerticies()
        {
            return _adjacencyMatrix.GetLength(0);
        }

        public void AddEdge(int vertex1, int vertex2, int weight)
        {
            AddEdge(vertex1, vertex2);
        }
    }
}