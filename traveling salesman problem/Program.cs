using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace traveling_salesman_problem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int GRAPH_SIZE = 150;
            const int MIN = 5, MAX = 50;

            /*Graph graph = new Graph(GRAPH_SIZE);
            graph.Fill(MIN, MAX);*/

            Graph graph = new Graph(@"C:\Users\Artem\Desktop\ПА\Lab3\traveling salesman problem\task.dat");

            Anthill anthill = new Anthill(graph);

            anthill.FindGoodTravel();

            graph.Write(@"C:\Users\Artem\Desktop\ПА\Lab3\traveling salesman problem\task.dat");

            

            Console.ReadKey();
        }
    }
}
