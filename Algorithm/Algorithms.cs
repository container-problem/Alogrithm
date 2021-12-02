using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    static class Algorithms
    {
        public static void swap(ref int x, ref int y)
        {
            var t = x;
            x = y;
            y = t;
        }

        public static List<List<int>> ExactAlg(int n, int M, int[] masses)
        {
            List<List<int>> bestSol = new List<List<int>>();
            int[] indices = new int[n];
            int[] indicesForBestSol = new int[n];
            for (int i = 0; i < n; i++)//2n+2
                indices[i] = i;
            int count = n;//1

            FindBest(0, n, masses, M, ref count, ref bestSol, ref indices, ref indicesForBestSol);//?
            //Т. к. распределение по контейнерам было найдено для массива,
            //отличного от исходного, необходимо переуказать индексы
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < bestSol[i].Count; j++)
                {
                    bestSol[i][j] = indicesForBestSol[bestSol[i][j]];
                }  
            }
                

            return bestSol;
        }

        public static List<List<int>> FFAlg(int n, int M, int[] masses)
        {
            return FF(n, M, masses);
        }

        public static List<List<int>> FFSAlg(int n, int M, int[] masses)
        {
            return FF(n, M, Sort.QuickSort(masses));
        }

        private static List<List<int>> FF(int n, int M, int[] masses)
        {
            List<List<int>> containers = new List<List<int>>();
            List<int> containersMasses = new List<int>();
            int k = 0; //кол-во использованных контейнеров //1
            bool IsFind;
            for (int i = 0; i < n; i++)//2n+2
            {
                IsFind = false;//n
                for (int j = 0; j < k; j++)//2n^2+
                {
                    if (M - containersMasses[j] >= masses[i])
                    {
                        containers[j].Add(i);
                        containersMasses[j] += masses[i];
                        IsFind = true;
                        break;
                    }
                }
                if (!IsFind) //предмет не был положен в контейнер
                {
                    List<int> newContainer = new List<int>();
                    newContainer.Add(i);
                    containers.Add(newContainer);
                    containersMasses.Add(masses[i]);
                    k++;
                }
            }
            return containers;
        }

        /// <summary>
        /// Полный перебор всех перестановок
        /// </summary>
        /// <param name="count">кол-во контейнеров</param>
        /// <param name="best">лучшее распределение предметов по контейнерам</param>
        /// <param name="index">порядок индексов согласно исходному массиву</param>
        /// <param name="bestIndex">порядок индексов для лучшего решения</param>
        private static void FindBest(int t, int n, int[] masses,
                int M, ref int count, ref List<List<int>> bestSol,
                ref int[] indices, ref int[] indicesForBestSol)
        {
            if (t == n - 1)
            {
                List<List<int>> result = FF(n, M, masses);
                if (result.Count <= count)
                {
                    bestSol = result;
                    count = result.Count;
                    Array.Copy(indices, indicesForBestSol, n);
                }
            }
            else
            {
                for (int j = t; j < n; ++j)
                {
                    swap(ref masses[t], ref masses[j]);
                    swap(ref indices[t], ref indices[j]);
                    t++;    
                    FindBest(t, n, masses, M, ref count, ref bestSol, ref indices, ref indicesForBestSol);
                    t--;
                    swap(ref masses[t], ref masses[j]);
                    swap(ref indices[t], ref indices[j]);
                }
            }
        }
    }
}

