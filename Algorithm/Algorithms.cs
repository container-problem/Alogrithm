using System;
using System.Collections.Generic;

public class Algorithms
{
    static void swap(ref int x, ref int y)
    {
        var t = x;
        x = y;
        y = t;
    }

    static void rename(int[] indices, ref List<List<int>> sol) //О(n)
    {
        for (int i = 0; i < sol.Count; i++)
            for (int j = 0; j < sol[i].Count; j++)
                sol[i][j] = indices[sol[i][j]];
    }

    static void FindBest(int t, int n, int[] masses,
            int M, ref int count, ref List<List<int>> bestSol,
            ref int[] indices, ref int[] indicesForBestSol) //О(n!)
    {
        if (t == n - 1)
        {
            List<List<int>> result = new List<List<int>>(FF(n, M, masses));
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

    public static List<List<int>> ExactAlg(int n, int M, int[] masses)//О(n!)
    {
        List<List<int>> bestSol = new List<List<int>>();
        int[] indices = new int[n];
        int[] indicesForBestSol = new int[n];
        for (int i = 0; i < n; i++)
            indices[i] = i;
        int count = n;

        FindBest(0, n, masses, M, ref count, ref bestSol, ref indices, ref indicesForBestSol);
        rename(indicesForBestSol, ref bestSol);
        return bestSol;
    }

    public static List<List<int>> FFAlg(int n, int M, int[] masses)//О(n^2)
    {
        return FF(n, M, masses);
    }

    public static List<List<int>> FFSAlg(int n, int M, int[] masses)//О(n^2)
    {
        List<List<int>> bestSol = new List<List<int>>();//О(n*log(n))
        int[] indices = new int[n];
        for (int i = 0; i < n; i++)
            indices[i] = i;
        bestSol = FF(n, M, Sort.QuickSort(masses, indices));
        rename(indices, ref bestSol);
        return bestSol;
    }

    static List<List<int>> FF(int n, int M, int[] masses)//О(n^2)
    {
        List<List<int>> containers = new List<List<int>>();
        List<int> containersMasses = new List<int>();
        int k = 0;
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
            if (!IsFind)
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


