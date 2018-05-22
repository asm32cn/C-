// cs-shell-sort-demo-1.cs
using System;

class ShellSortDemo1{
    public static void Main(string[] args){
        // int[] data = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36};
        int[] data = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82};

        ShellSortDemo1 ssd = new ShellSortDemo1();
        ssd.DisplayData(data);
        ssd.ShellSort(data);
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

    public void ShellSort(int[] data){
        int n = data.Length;
        int h = 0;
        while(h <= n){
            h = 3 * h + 1;
        }
        while(h >= 1){
            for(int i = h; i < n; i++){
                int j = i - h;
                int nGet = data[i];
                while(j >= 0 && data[j] > nGet){
                    data[j + h] = data[j];
                    j = j - h;
                }
                data[j + h] = nGet;
            }
            h = (h - 1) / 3;
        }

    }
}