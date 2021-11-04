using System;
using System.Collections.Generic;
using System.Linq;

namespace traveling_salesman_problem
{
    internal class Anthill
    {
        private Graph distance;
        private Graph pheromone;

        // algorithm properties 
        private readonly double alpha = 2;
        private readonly double beta = 3;
        private readonly double startingPheromon = 1;
        private readonly double pheromonEvaporation = 0.4;
        public double Lmin { get; private set; }
        private readonly int numberOfAnts = 35; // num of ants

        Random random = new Random();
        public Anthill(Graph distance)
        {
            this.distance = distance;
            pheromone = new Graph(distance.Size);
            InitPheromone(startingPheromon);
            Lmin = GreedySearch();
        }
        private void InitPheromone(double startingPheromon)
        {
            pheromone.Fill(startingPheromon);
        }
        private double GreedySearch()
        {
            HashSet<int> visited = new();
            double Lmin = 0;
            int size = distance.Size;
            int cur = random.Next(size); // start from 
            int closestVertex = cur;

            for (int k = 0; k < size; k++)
            {
                visited.Add(cur);
                double min = double.MaxValue;
                for (int i = 0; i < size; i++)
                {
                    if (distance[cur, i] < min && !visited.Contains(i))
                    {
                        min = distance[cur, i];
                        closestVertex = i;
                    }
                }
                if (cur != closestVertex)
                {
                    Lmin += min;
                    cur = closestVertex;
                }
            }
            return Lmin;
        }
        public void FindGoodTravel()
        {
            List<int> bestPath = new();
            double bestValueOfPath = int.MaxValue;

            const int ITERATIONS = 1000;
            for (int k = 0; k < ITERATIONS; k++)
            {
                List<Ant> ants = CreateAnts();
                foreach (var ant in ants)
                {
                    while (ant.GetVisited() < distance.Size) // all vertexes were visited
                    {
                        ant.Step(pheromone, distance, alpha, beta);
                    }
                }
                RenewPheromon(ants);

                double path = ants.Min(x => x.ValueOfPath);
                if (path < bestValueOfPath)
                {
                    bestValueOfPath = path;
                }
                if (k % 20 == 0)
                {
                    Console.WriteLine(bestValueOfPath);
                }
            }
        }
        private List<Ant> CreateAnts()
        {
            List<Ant> ants = new();
            for (int i = 0; i < numberOfAnts; i++)
            {
                int startLocation = random.Next(distance.Size);
                ants.Add(new Ant(startLocation));
            }
            return ants;
        }
        private void RenewPheromon(List<Ant> ants)
        {
            // evaporate
            for (int i = 0; i < distance.Size; i++)
            {
                for (int j = i + 1; j < distance.Size; j++)
                {
                    pheromone[i, j] = pheromone[j, i] *= 1 - pheromonEvaporation;
                }
            }
            //lay pheromon
            foreach (var ant in ants)
            {
                for (int i = 1; i < ant.Path.Count; i++)
                {
                    int cur = ant.Path[i - 1];
                    int next = ant.Path[i];
                    pheromone[cur, next] = pheromone[next, cur] += Lmin / ant.ValueOfPath;
                }
            }
        }
    }
}
