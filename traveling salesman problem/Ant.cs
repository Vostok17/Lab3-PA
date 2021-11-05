using System;
using System.Collections.Generic;

namespace traveling_salesman_problem
{
    internal class Ant : IComparable<Ant>
    {
        private int location;
        private int start;
        public List<int> Path { get; set; } = new();
        private List<int> possibleWays = new();
        public int ValueOfPath { get; private set; }
        public Ant(int startLocation, int size)
        {
            start = startLocation;
            location = start;
            ValueOfPath = 0;
            InitWays(size);
        }

        private void InitWays(int size)
        {
            for (int i = 0; i < size; i++)
            {
                if (i != start) possibleWays.Add(i);
            }
        }
        public void Step(double[,] pheromone, Graph distance, int alpha, int beta)
        {
            Path.Add(location);
            possibleWays.Remove(location);

            double summary = 0;
            foreach (int way in possibleWays)
            {
                summary += Math.Pow(pheromone[location, way], alpha) * Math.Pow((double)1 / distance[location, way], beta);
            }

            int nextVertex = int.MaxValue;
            double maxProbability = double.MinValue;
            foreach (int way in possibleWays)
            {
                double probability = Math.Pow(pheromone[location, way], alpha) * Math.Pow((double)1 / distance[location, way], beta) / summary;
                if (probability > maxProbability)
                {
                    maxProbability = probability;
                    nextVertex = way;
                }
            }

            if (nextVertex != int.MaxValue)
            {
                ValueOfPath += distance[location, nextVertex];
                location = nextVertex;
            }
            else // must return to start
            {
                ValueOfPath += distance[location, start];
                Path.Add(start);
            }
        }
        public bool CanMove() => possibleWays.Count != 0;

        public int CompareTo(Ant other)
        {
            return ValueOfPath.CompareTo(other.ValueOfPath);
        }
    }
}
