﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace traveling_salesman_problem
{
    internal class Ant : IComparable<Ant>
    {
        private int location;
        private int start;
        public List<int> Path { get; set; } = new();
        private HashSet<int> visited = new();
        public int ValueOfPath { get; private set; }
        public Ant(int startLocation)
        {
            start = startLocation;
            location = start;
            ValueOfPath = 0;
        }
        public void Step(double[,] pheromone, Graph distance, int alpha, int beta)
        {
            Path.Add(location);
            visited.Add(location);

            int nextVertex = int.MaxValue;
            double maxProbability = double.MinValue;
            for (int i = 0; i < distance.Size; i++)
            {
                if (!visited.Contains(i))
                {
                    double probability = Math.Pow(pheromone[location, i], alpha) + Math.Pow((double)1 / distance[location, i], beta);
                    if (probability > maxProbability)
                    {
                        maxProbability = probability;
                        nextVertex = i;
                    }
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
        public int GetVisited() => visited.Count;

        public int CompareTo(Ant other)
        {
            return ValueOfPath.CompareTo(other.ValueOfPath);
        }
    }
}
