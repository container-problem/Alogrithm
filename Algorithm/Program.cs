﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
                throw e;
            }
        }

        /*
        static void Main(string[] args)
        {
            int a = int.MaxValue;
            TimeSpan sum = TimeSpan.Zero;
            var start = DateTime.Now;
            for (int i = 0; i < 1000000; i++)
            {
                a--;
                a++;
                a /= 2;
                a *= 2;
                
            }
            var end = DateTime.Now;
            sum += end - start;
           

            Console.WriteLine((sum.TotalMilliseconds / 1000000 )/6);

            Console.ReadKey();
        }
        */

        /*
        static void Main(string[] args)
        {
            
            
            var random = new Random();

            Console.Write("Read from file = ");
            bool readFromFile = bool.Parse(Console.ReadLine());
            var input = "";
            if(readFromFile)
            {
                Console.Write("Input file path = ");
                input = Console.ReadLine();
                Read(input);
            }

            M = 100;
            int N = readFromFile ? n : 12;
            for (int i = 1; i < N; i++)
            {
                if(!readFromFile) 
                {
                    n = i;
                    masses = new int[n];
                }

                Console.WriteLine($"For N = {n}");

                double mediumDeviationFF = 0;
                double mediumDeviationFFS = 0;
                for (int k = 0; k < 100; k++)
                {
                    if(!readFromFile)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            masses[j] = random.Next(1, M);
                        }
                    }
                    

                    double exactSolution = F(Algorithms.ExactAlg(n, M, masses));
                    double FFSolution = F(Algorithms.FFAlg(n, M, masses));
                    double FFSSolution = F(Algorithms.FFSAlg(n, M, masses));

                    mediumDeviationFF = exactSolution * 1.7 - FFSolution;
                    mediumDeviationFFS = 11.0 / 9.0 * exactSolution + 1 - FFSSolution;

                    if (mediumDeviationFF < 0)
                    {
                        Console.WriteLine("FF вышел за пределы");
                    }

                    if (mediumDeviationFFS < 0)
                    {
                        Console.WriteLine("FFS вышел за пределы");
                    }
                }
            }

            Console.ReadKey();
        }

        private static double F(List<List<int>> solution)
        {
            return solution.Count();
        }
        
        */

        //Оценка времени выполнения
        static void Main(string[] args)
        {
            Console.Write("Read from file = ");
            bool readFromFile = bool.Parse(Console.ReadLine());

            M = 100;
            

            if (readFromFile)
            {
                Console.Write("Input file path = ");
                string input = Console.ReadLine();
                Read(input);
            }

            int N = readFromFile ? n : 12;

            using (var sw = new StreamWriter("output.txt"))
            {
                

                var random = new Random();
                for (int i = 1; i < N; i++)
                {
                    if(!readFromFile)
                    {
                        n = i;
                        masses = new int[n];
                    }

                    Console.WriteLine($"For N = {n}");
                    
                    TimeSpan Sum = TimeSpan.Zero;
                    
                    for (int k = 0; k < 100; k++)
                    {
                        if(!readFromFile)
                        {
                            for (int j = 0; j < n; j++)
                            {
                                masses[j] = random.Next(1, M);
                            }
                        }
                        

                        DateTime start = DateTime.Now;
                        Algorithms.ExactAlg(n, M, masses);
                        DateTime end = DateTime.Now;
                        Sum += end - start;
                    }

                    Console.WriteLine($"Time for exact algorithm: {(Sum / 100).TotalMilliseconds}");
                    
                    //Sum = TimeSpan.Zero;
                    for (int k = 0; k < 1000000; k++)
                    {
                        if (!readFromFile)
                        {
                            for (int j = 0; j < n; j++)
                            {
                                masses[j] = random.Next(1, M);
                            }
                        }

                        DateTime start = DateTime.Now;
                        Algorithms.FFAlg(n, M, masses);
                        DateTime end = DateTime.Now;
                        Sum += end - start;
                    }


                    string line = $"{n},{(Sum).TotalMilliseconds / 1000000}";



                    Sum = TimeSpan.Zero;
                    for (int k = 0; k < 1000000; k++)
                    {
                        if (!readFromFile)
                        {
                            for (int j = 0; j < n; j++)
                            {
                                masses[j] = random.Next(1, M);
                            }
                        }

                        DateTime start = DateTime.Now;
                        Algorithms.FFSAlg(n, M, masses);
                        DateTime end = DateTime.Now;
                        Sum += end - start;
                    }

                    line += $",{(Sum).TotalMilliseconds / 1000000} ";

                    sw.WriteLine(line);
                }

            }
            Console.ReadKey();
        }
    

    }
}
