// cs-bubblesort-demo-1.cs
using System;

public class BubbleSortDemo1{
    public static void Main(string[] args){
        // int[] data ={41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36};
        int[] data = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82};

        BubbleSortDemo1 bsd = new BubbleSortDemo1();
        bsd.DisplayData(data);
        bsd.BubbleSort(data);
        bsd.DisplayData(data);
    }

    void DisplayData(int[] data){
        int n = data.Length;
        for(int i = 0; i < n; i++){
            if(i > 0) Console.Write(", ");
            Console.Write(data[i]);
        }
        Console.WriteLine();
    }

    void BubbleSort(int[] data){
        int n = data.Length;
        for(int j = 0; j < n - 1; j++){
            for(int i = 0; i < n - 1 - j; i++){
                if(data[i] > data[i + 1]){
                    int temp = data[i];
                    data[i] = data[i + 1];
                    data[i + 1] = temp;
                }
            }
        }
    }
}