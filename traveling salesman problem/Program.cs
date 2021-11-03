using System;

namespace traveling_salesman_problem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph(@"C:\Users\Artem\Desktop\ПА\Lab3\traveling salesman problem\graph.txt");
            Anthill anthill = new Anthill(graph);

            anthill.FindGoodTravel();

            Console.ReadKey();
        }
    }
}
