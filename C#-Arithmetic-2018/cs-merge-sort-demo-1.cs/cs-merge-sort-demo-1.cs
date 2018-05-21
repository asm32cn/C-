// cs-merge-sort-demo-1.cs
using System;

class MergeSortDemo1{
    public static void Main(string[] args){
        // int[] data = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36};
        int[] data1 = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82};
        int[] data2 = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82};

        MergeSortDemo1 msd = new MergeSortDemo1();
        msd.DisplayData(data1);
        msd.MergeSortRecursion(data1, 0, data1.Length - 1);
        msd.DisplayData(data1);

        msd.DisplayData(data2);
        msd.MergeSortIteration(data2);
        msd.DisplayData(data2);
    }

    public void DisplayData(int[] data){
        int n = data.Length;
        for(int i = 0; i < n; i++){
            if(i > 0) Console.Write(", ");
            Console.Write(data[i]);
        }
        Console.WriteLine();
    }

    private void Merge(int[] data, int nLeft, int nMid, int nRight){
        int nLen = nRight - nLeft + 1;
        int[] temp = new int[nLen];
        int nIndex = 0;
        int i = nLeft;
        int j = nMid + 1;
        while(i <= nMid && j <= nRight){
            temp[nIndex++] = data[i] <= data[j] ? data[i++] : data[j++];
        }
        while(i <= nMid){
            temp[nIndex++] = data[i++];
        }
        while(j <= nRight){
            temp[nIndex++] = data[j++];
        }
        for(int k = 0; k < nLen; k++){
            data[nLeft++] = temp[k];
        }
    }

    // 递归实现的归并排序(自顶向下)
    public void MergeSortRecursion(int[] data, int nLeft, int nRight){
        // 当待排序的序列长度为1时，递归开始回溯，进行merge操作
        if(nLeft == nRight) return;

        int nMid = (nLeft + nRight) / 2;
        MergeSortRecursion(data, nLeft, nMid);
        MergeSortRecursion(data, nMid + 1, nRight);
        Merge(data, nLeft, nMid, nRight);
    }

    // 非递归(迭代)实现的归并排序(自底向上)
    public void MergeSortIteration(int[] data){
        int n = data.Length;
        // 子数组索引，前一个为A[left ... mid]，后一个为A[mid + 1 ... right]
        int nLeft, nMid, nRight;
        // 子数组的大小i初始为1，没轮翻倍
        for(int i = 1; i < n; i *= 2){
            nLeft = 0;
            // 后一个子数组存在(需要归并)
            while(nLeft + i < n){
                nMid = nLeft + i - 1;
                // 后一个子数组大小可能不够
                nRight = nMid + i < n ? nMid + i : n - 1;
                Merge(data, nLeft, nMid, nRight);
                // 前一个子数组索引向后移动
                nLeft = nRight + 1;
            }
        }
    }
}