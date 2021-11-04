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
        public void Fill(int min, int max)
        {
            Random rdn = new();
            for (int i = 0; i < Size; i++)
            {
                for (int j = i + 1; j < Size; j++)
                {
                    Matrix[i, j] = Matrix[j, i] = Convert.ToDouble(rdn.Next(min, max + 1));
                }
            }
        }
        public void Fill(double sameValue)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = i + 1; j < Size; j++)
                {
                    Matrix[i, j] = Matrix[j, i] = sameValue;
                }
            }
        }
    }
}
