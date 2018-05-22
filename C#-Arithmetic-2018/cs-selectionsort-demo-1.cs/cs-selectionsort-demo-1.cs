// cs-selectionsort-demo-1.cs
using System;

class SelectionSortDemo1{
    public static void Main(string[] args){
        // int[] data = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36};
        int[] data = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82};

        SelectionSortDemo1 ssd = new SelectionSortDemo1();
        ssd.DisplayData(data);
        ssd.SelectionSort(data);
        ssd.DisplayData(data);
    }

    public void DisplayData(int[] data){
        int n = data.Length;
        for(int i = 0; i < n; i++){
            if(i > 0) Console.Write(", ");
            Console.Write(data[i]);
        }
        Console.WriteLine();
    }

    public void SelectionSort(int[] data){
        int n = data.Length;
        for(int i = 0; i < n - 1; i++){
            int nMin = i;
            for(int j = i + 1; j < n; j++){
                if(data[j] < data[nMin]){
                    nMin = j;
                }
            }
            if(nMin != i){
                int temp = data[i];
                data[i] = data[nMin];
                data[nMin] = temp;
            }
        }

    }
}