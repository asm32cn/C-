// cs-cocktailsort-demo-1.cs
using System;

class CocktailSortDemo1{
    public static void Main(string[] args){
        // int[] data = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36};
        int[] data = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82};
        CocktailSortDemo1 csd = new CocktailSortDemo1();

        csd.DisplayData(data);
        csd.CocktailSort(data);
        csd.DisplayData(data);
    }

    public void DisplayData(int[] data){
        int n = data.Length;
        for(int i = 0; i < n; i++){
            if(i > 0) Console.Write(", ");
            Console.Write(data[i]);
        }
        Console.WriteLine();
    }

    public void Swap(int[] data, int i, int j){
        int temp = data[i];
        data[i] = data[j];
        data[j] = temp;
    }

    public void CocktailSort(int[] data){
        int n = data.Length;
        int nLeft = 0;
        int nRight = n - 1;
        while(nLeft < nRight){
            for(int i = nLeft; i < nRight; i++){
                if(data[i] > data[i + 1]){
                    Swap(data, i, i + 1);
                }
            }
            nRight--;
            for(int i = nRight; i > nLeft; i--){
                if(data[i - 1] > data[i]){
                    Swap(data, i - 1, i);
                }
            }
            nLeft++;
        }

    }
}