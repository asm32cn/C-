// cs-quicksort-demo-1.cs
using System;

class QuickSortDemo1{
    public static void Main(string[] args){
        // int[] data = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36};
        int[] data = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82};

        QuickSortDemo1 qsd = new QuickSortDemo1();
        qsd.DisplayData(data);
        qsd.QuickSort(data, 0, data.Length - 1);
        qsd.DisplayData(data);
    }

    public void DisplayData(int[] data){
        int n = data.Length;
        for(int i = 0; i < n; i++){
            if(i > 0) Console.Write(", ");
            Console.Write(data[i]);
        }
        Console.WriteLine();
    }

    public void QuickSort(int[] data, int nLeft, int nRight){
        if(nLeft < nRight){
            int nKey = data[nLeft];
            int nLow = nLeft;
            int nHigh = nRight;
            while(nLow < nHigh){
                while(nLow < nHigh && data[nHigh] >= nKey){
                    nHigh--;
                }
                data[nLow] = data[nHigh];
                while(nLow < nHigh && data[nLow] <= nKey){
                    nLow++;
                }
                data[nHigh] = data[nLow];
            }
            data[nLow] = nKey;

            QuickSort(data, nLeft, nLow - 1);
            QuickSort(data, nLow + 1, nRight);
        }
    }
}