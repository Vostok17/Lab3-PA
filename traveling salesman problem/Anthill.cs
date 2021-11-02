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
        private const int alpha = 2;
        private const int beta = 3;
        private const double startingPheromon = 1;
        private const double pheromonEvaporation = 0.4;
        private double Lmin;
        private const int M = 35; // num of ants

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

        // TODO 
        public void FindGoodTravel()
        {

        }
    }
}
