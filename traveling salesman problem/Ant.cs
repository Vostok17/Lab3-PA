using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ValueOfPath = 0;
        }
        public void Step(Graph pheromone, Graph distance, double alpha, double beta)
        {
            List<double> neighbours = new();
            for (int i = 0; i < distance.Size; i++)
            {
                if (!visited.Contains(i))
                {
                    double probability = Math.Pow(pheromone[location, i], alpha) + Math.Pow(1 / distance[location, i], beta);
                    neighbours.Add(probability);
                }
            }
            double maxProbability = neighbours.Max();
            int nextVertex = neighbours.IndexOf(maxProbability);

            Path.Add(location);
            ValueOfPath += distance[location, nextVertex];
            visited.Add(location);

            location = nextVertex;
        }
        public int GetVisited() { return visited.Count; }
    }
}
