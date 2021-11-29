using System;
using System.IO;

namespace Algorithm
{
    class Program
    {
        public static int n;
        public static int M;
        public static int[] masses;

        public static void Read(string path)
        {
            try
            {
                string[] strArray;
                using (StreamReader sr = new StreamReader(path))
                {
                    strArray = sr.ReadLine().Split(' ');
                    n = Convert.ToInt32(strArray[0]);
                    M = Convert.ToInt32(strArray[1]);
                    masses = new int[n];
                    strArray = sr.ReadLine().Split(' ');
                    for (int i = 0; i < n; i++)
                    {
                        masses[i] = Convert.ToInt32(strArray[i]);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при чтении файла: \n" + e.Message);
            }
        }

        static void Main(string[] args)
        {
            string path = Console.ReadLine();
            Read(path);
        }
    }
}
