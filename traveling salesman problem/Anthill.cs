using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace traveling_salesman_problem
{
    internal class Anthill
    {
        private Graph distance; // graph
        private double[,] pheromone;

        // algorithm properties 
        private readonly int alpha = 2;
        private readonly int beta = 3;
        private readonly double startingPheromon = 2;
        private readonly double pheromonEvaporation = 0.4;
        private int Lmin;
        private readonly int numberOfAnts = 35;

        private Random random = new Random();
        public List<int> Result { get; private set; } = new();
        public Anthill(Graph graph)
        {
            distance = graph;
            InitPheromone();
            Lmin = GreedySearch();
        }
        private void InitPheromone()
        {
            int size = distance.Size;
            pheromone = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = i + 1; j < size; j++)
                {
                    pheromone[i, j] = pheromone[j, i] = startingPheromon;
                }
            }
        }
        private int GreedySearch()
        {
            HashSet<int> visited = new();
            int Lmin = 0;
            int size = distance.Size;
            int cur = random.Next(size); // start from random vertex
            int closestVertex = cur;

            for (int k = 0; k < size; k++)
            {
                visited.Add(cur);
                int min = int.MaxValue;
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
            Stopwatch time = new Stopwatch();
            time.Start();

            double bestPathValue = int.MaxValue;

            const int ITERATIONS = 1000;
            for (int k = 0; k <= ITERATIONS; k++)
            {
                List<Ant> ants = CreateAnts();
                foreach (var ant in ants)
                {
                    while (ant.CanMove())
                    {
                        ant.Step(pheromone, distance, alpha, beta);
                    }
                }
                RenewPheromon(ants);

                Ant shortPath = ants.Min(); // ant with shortest path on this iteration

                if (shortPath.ValueOfPath < bestPathValue)
                {
                    bestPathValue = shortPath.ValueOfPath;
                    Result = shortPath.Path;
                }
                if (k % 20 == 0 || k < 20)
                {
                    Console.WriteLine($"{k}. {bestPathValue}");
                }
            }

            // statistics
            Console.WriteLine("Best path value: {0}", bestPathValue);
            Console.WriteLine("Lmin = {0}", Lmin);
            foreach (var item in Result)
            {
                Console.Write(item + "-");
            }
            Console.WriteLine();
            time.Stop();
            Console.WriteLine($"time: {time.Elapsed}");
        }
        private List<Ant> CreateAnts()
        {
            List<Ant> ants = new();
            for (int i = 0; i < numberOfAnts; i++)
            {
                int startLocation = random.Next(distance.Size);
                ants.Add(new Ant(startLocation, distance.Size));
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
                    pheromone[cur, next] = pheromone[next, cur] += (double)Lmin / ant.ValueOfPath;
                }
            }
        }
    }
}
