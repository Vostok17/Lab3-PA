using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traveling_salesman_problem
{
    internal class Anthill
    {
        private Graph distance;
        private Graph pheromone;

        // algorithm properties 
        private const double ALPHA = 2;
        private const double BETA = 3;
        private const double STARTING_PHEROMON = 1;
        private const double PHEROMON_EVAPORATION = 0.4;
        private double Lmin;
        private const int M = 35; // num of ants

        Random random = new Random();
        public Anthill(Graph distance)
        {
            this.distance = distance;
            pheromone = new Graph(distance.Size);
            InitPheromone(STARTING_PHEROMON);
            Lmin = GreedySearch();
        }
        private void InitPheromone(double startingPheromon)
        {
            for (int i = 0; i < pheromone.Size; i++)
            {
                for (int j = 0; j < pheromone.Size; j++)
                {
                    pheromone[i, j] = startingPheromon;
                }
            }
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

        // UNDONE 
        public void FindGoodTravel()
        {
            const int ITERATIONS = 1;
            for (int k = 0; k < ITERATIONS; k++)
            {
                List<Ant> ants = CreateAnts();
                foreach (var ant in ants)
                {
                    while (ant.GetVisited() < distance.Size) // not all vertexes were visited
                    {
                        ant.Step(pheromone, distance, ALPHA, BETA);
                    }
                    LayPheromon(ant.Path);
                    Evaporate();
                }
            }
        }
        private List<Ant> CreateAnts()
        {
            List<Ant> ants = new();
            for (int i = 0; i < M; i++)
            {
                int startLocation = random.Next(distance.Size);
                ants.Add(new Ant(startLocation));
            }
            return ants;
        }
        private void LayPheromon(List<int> path)
        {
            for (int i = 1; i < path.Count; i++)
            {
                pheromone[path[i - 1], path[i]] += Lmin / path.Count;
            }
        }
        private void Evaporate()
        {
            for (int i = 0; i < distance.Size; i++)
            {
                for (int j = 0; j < distance.Size; j++)
                {
                    pheromone[i, j] *= 1 - PHEROMON_EVAPORATION;
                }
            }
        }
    }
}
