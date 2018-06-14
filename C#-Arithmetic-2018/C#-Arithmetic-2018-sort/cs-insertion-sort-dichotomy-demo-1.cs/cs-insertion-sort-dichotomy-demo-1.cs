// cs-insertion-sort-dichotomy-demo-1.cs
using System;

class InsertionSortDichotomyDemo1{
    public static void Main(string[] args){
        // int[] data = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36};
        int[] data = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82};

        InsertionSortDichotomyDemo1 isdd = new InsertionSortDichotomyDemo1();
        isdd.DisplayData(data);
        isdd.InsertionSortDichotomy(data);
        isdd.DisplayData(data);
    }

    public void DisplayData(int[] data){
        int n = data.Length;
        for(int i = 0; i < n; i++){
            if(i > 0) Console.Write(", ");
            Console.Write(data[i]);
        }
        Console.WriteLine();
    }

    public void InsertionSortDichotomy(int[] data){
        int n = data.Length;
        for(int i = 1; i < n; i++){
            int nGet = data[i];
            int nLeft = 0;
            int nRight = i - 1;
            while(nLeft <= nRight){
                int nMid = (nLeft + nRight) / 2;
                if(data[nMid] > nGet){
                    nRight = nMid - 1;
                }else{
                    nLeft = nMid + 1;
                }
            }
            for(int j = i - 1; j >= nLeft; j--){
                data[j + 1] = data[j];
            }
            data[nLeft] = nGet;
        }
    }
}