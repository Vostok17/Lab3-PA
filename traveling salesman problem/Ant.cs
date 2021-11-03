using System;
using System.Collections.Generic;
using System.Linq;

namespace traveling_salesman_problem
{
    internal class Ant
    {
        private int location;
        private int start;
        public List<int> Path = new();
        private HashSet<int> visited = new();
        public double ValueOfPath { get; private set; }
        public Ant(int startLocation)
        {
            start = startLocation;
            location = start;
            ValueOfPath = 0;
        }
        public void Step(Graph pheromone, Graph distance, double alpha, double beta)
        {
            Path.Add(location);
            visited.Add(location);

            Dictionary<int, double> neighbours = new();
            for (int i = 0; i < distance.Size; i++)
            {
                if (!visited.Contains(i))
                {
                    double probability = Math.Pow(pheromone[location, i], alpha) + Math.Pow(1 / distance[location, i], beta);
                    neighbours.Add(i, probability);
                }
            }
            int nextVertex = neighbours.FirstOrDefault(x => x.Value == neighbours.Values.Max()).Key;

            if (neighbours.Count != 0)
            {
                ValueOfPath += distance[location, nextVertex];
                location = nextVertex;
            }
            else // must return to start
            {
                Path.Add(start);
                ValueOfPath += distance[location, start];
            }
        }
        public int GetVisited() { return visited.Count; }
    }
}
