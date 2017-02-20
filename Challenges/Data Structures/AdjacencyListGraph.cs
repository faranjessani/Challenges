using System.Collections.Generic;

namespace Challenges
{
    public class AdjacencyListGraph : IGraph
    {
        private readonly HashSet<int>[] _adjacencyList;

        public AdjacencyListGraph(int size)
        {
            _adjacencyList = new HashSet<int>[size];
            for (int i = 0; i < size; i++)
            {
                _adjacencyList[i] = new HashSet<int>();
            }
        }

        public void AddEdge(int vertex1, int vertex2)
        {
            _adjacencyList[vertex1].Add(vertex2);
            _adjacencyList[vertex2].Add(vertex1);
        }

        public IEnumerable<int> GetAdjacentVerticies(int vertex)
        {
            return _adjacencyList[vertex];
        }

        public int GetNumberOfVerticies()
        {
            return _adjacencyList.Length;
        }

        public void AddEdge(int vertex1, int vertex2, int weight)
        {
            AddEdge(vertex1, vertex2);
        }
    }
}
