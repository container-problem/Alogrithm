using System;

public class Sort
{
    //метод для обмена элементов массива
    static void Swap(ref int x, ref int y)
    {
        var t = x;
        x = y;
        y = t;
    }

    //метод возвращающий индекс опорного элемента
    static int Partition(int[] array, int minIndex, int maxIndex, int[] indices)
    {
        var pivot = minIndex - 1;
        for (var i = minIndex; i < maxIndex; i++)
        {
            if (array[i] > array[maxIndex])
            {
                pivot++;
                Swap(ref array[pivot], ref array[i]);
                Swap(ref indices[pivot], ref indices[i]);
            }
        }

        pivot++;
        Swap(ref array[pivot], ref array[maxIndex]);
        Swap(ref indices[pivot], ref indices[maxIndex]);
        return pivot;
    }

    static int[] QuickSort(int[] array, int minIndex, int maxIndex, int[] indices)
    {
        if (minIndex >= maxIndex)
        {
            return array;
        }

        var pivotIndex = Partition(array, minIndex, maxIndex, indices);
        QuickSort(array, minIndex, pivotIndex - 1, indices);
        QuickSort(array, pivotIndex + 1, maxIndex, indices);

        return array;
    }

    public static int[] QuickSort(int[] array, int[] indices)
    {
        return QuickSort(array, 0, array.Length - 1, indices);
    }
}
