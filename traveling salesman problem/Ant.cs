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
        private Random random = new Random();
        public Ant(int startLocation, int size)
        {
            start = startLocation;
            location = start;
            ValueOfPath = 0;
            InitWays(size);

            Path.Add(start);
            possibleWays.Remove(start);
        }

        private void InitWays(int size)
        {
            for (int i = 0; i < size; i++)
            {
                possibleWays.Add(i);
            }
        }
        public void Step(double[,] pheromone, Graph distance, int alpha, int beta)
        {
            double summary = 0;
            foreach (int way in possibleWays)
            {
                summary += Math.Pow(pheromone[location, way], alpha) * Math.Pow((double)1 / distance[location, way], beta);
            }

            int nextVertex = int.MaxValue;
            double probability = 0;
            double randomProbability = random.NextDouble();
            foreach (int way in possibleWays)
            {
                probability += Math.Pow(pheromone[location, way], alpha) * Math.Pow((double)1 / distance[location, way], beta) / summary;
                if (probability >= randomProbability)
                {
                    nextVertex = way;
                    break;
                }
            }

            ValueOfPath += distance[location, nextVertex];
            location = nextVertex;
            Path.Add(nextVertex);
            possibleWays.Remove(nextVertex);
        }
        public bool CanMove() => possibleWays.Count != 0;
        public void ReturnToStart(Graph distance)
        {
            ValueOfPath += distance[location, start];
            Path.Add(start);
        }

        public int CompareTo(Ant other)
        {
            return ValueOfPath.CompareTo(other.ValueOfPath);
        }
    }
}
