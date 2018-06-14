// cs-insertion-sort-demo-1.cs
using System;

class InsertionSortDemo1{
    public static void Main(string[] args){
        // int[] data = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36};
        int[] data = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82};

        InsertionSortDemo1 isd = new InsertionSortDemo1();
        isd.DisplayData(data);
        isd.InsertionSort(data);
        isd.DisplayData(data);
    }

    public void DisplayData(int[] data){
        int n = data.Length;
        for(int i = 0; i < n; i++){
            if(i > 0) Console.Write(", ");
            Console.Write(data[i]);
        }
        Console.WriteLine();
    }

    public void InsertionSort(int[] data){
        int n = data.Length;
        for(int i = 0; i < n; i++){
            int nGet = data[i];
            int j = i - 1;
            while(j >= 0 && data[j] > nGet){
                data[j + 1] = data[j];
                j--;
            }
            data[j + 1] = nGet;
        }
    }
}