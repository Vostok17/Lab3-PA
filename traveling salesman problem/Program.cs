using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace traveling_salesman_problem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch time = new Stopwatch();
            time.Start();

            Graph graph = new Graph(150);
            graph.Fill(5, 50);

            Anthill anthill = new Anthill(graph);

            anthill.FindGoodTravel();
            time.Stop();
            Console.WriteLine("Lmin = {0}", anthill.Lmin);
            Console.WriteLine(time.Elapsed);

            Console.ReadKey();
        }
    }
}
