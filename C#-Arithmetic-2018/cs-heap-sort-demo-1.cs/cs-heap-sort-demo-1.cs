// cs-heap-sort-demo-1.cs
using System;

class HeapSortDemo1{
    public static void Main(string[] args){
        // int[] data = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36};
        int[] data = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82};

        HeapSortDemo1 hsd = new HeapSortDemo1();
        hsd.DisplayData(data);
        hsd.HeapSort(data);
        hsd.DisplayData(data);
    }

    public void DisplayData(int[] data){
        int n = data.Length;
        for(int i = 0; i < n; i++){
            if(i > 0) Console.Write(", ");
            Console.Write(data[i]);
        }
        Console.WriteLine();
    }

    public void HeapSort(int[] data){
        int nHeapSize = data.Length;
        // BuildHeap
        for(int i = nHeapSize / 2 - 1; i >= 0; i--){
            Heapify(data, i, nHeapSize);
        }
        // HeapSort
        while(nHeapSize > 0){
            Swap(data, 0, --nHeapSize);
            Heapify(data, 0, nHeapSize);
        }
    }

    private void Swap(int[] data, int i, int j){
        int temp = data[i];
        data[i] = data[j];
        data[j] = temp;
    }

    private void Heapify(int[] data, int i, int nSize){
        int nLeftChild = 2 * i + 1;
        int nRightChild = 2 * i + 2;
        int nMax = i;
        if(nLeftChild < nSize && data[nLeftChild] > data[nMax]){
            nMax = nLeftChild;
        }
        if(nRightChild < nSize && data[nRightChild] > data[nMax]){
            nMax = nRightChild;
        }
        if(nMax != i){
            Swap(data, i, nMax);
            Heapify(data, nMax, nSize);
        }
    }
}