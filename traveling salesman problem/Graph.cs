using System;
using System.Globalization;
using System.IO;

namespace traveling_salesman_problem
{
    internal class Graph
    {
        public double[,] Matrix { get; set; }
        public int Size { get; private set; }
        public double this[int i, int j]
        {
            get
            {
                return Matrix[i, j];
            }
            set
            {
                Matrix[i, j] = value;
            }
        }
        public Graph(string path)
        {
            Read(path);
        }
        public Graph(int size)
        {
            Size = size;
            Matrix = new double[Size, Size];
        }

        private void Read(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                // get size
                Size = Convert.ToInt32(sr.ReadLine());
                // read matrix
                Matrix = new double[Size, Size];
                for (int i = 0; i < Size; i++)
                {
                    string line = sr.ReadLine();
                    string[] row = line.Split(' ');
                    for (int j = 0; j < row.Length; j++)
                    {
                        Matrix[i, j] = double.Parse(row[j], CultureInfo.InvariantCulture);
                    }
                }
            }
        }
    }
}
