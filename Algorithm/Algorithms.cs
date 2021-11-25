using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    static class Algorithms
    {
        public static List<List<int>> FF(int n, int M, int[] masses)
        {
            List<List<int>> containers = new List<List<int>>();
            List<int> containersMasses = new List<int>();
            int k = 0; //кол-во использованных контейнеров
            bool IsFind;
            for (int i = 0; i < n; i++)
            {
                IsFind = false;
                for (int j = 0; j < k; j++)
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
    }
}

