// cs-bucket-sort-demo-1.cs
using System;

public class BucketSortDemo1{
	const int MAX = 100;
	const int bn = 5;
	const int nFactor = MAX % bn == 0 ? MAX / bn + 1 : MAX / bn;
	int[] C = new int[bn];

	public static void Main(string[] args){
		// int[] data = {41, 67, 34, 0, 69, 24, 78, 58, 62, 64, 5, 45, 81, 27, 61, 91, 95, 42, 27, 36};
		int[] data = {76, 11, 11, 43, 78, 35, 39, 27, 16, 55, 1, 41, 24, 19, 54, 7, 78, 69, 65, 82};

		BucketSortDemo1 bsd = new BucketSortDemo1();
		bsd.DisplayData(data);
		bsd.BucketSort(data);
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

	void InsertionSort(int[] data, int nLeft, int nRight){
		for(int i = nLeft + 1; i <= nRight; i++){
			int nGet = data[i];
			int j = i - 1;
			while(j >= nLeft && data[j] > nGet){
				data[j + 1] = data[j];
				j--;
			}
			data[j + 1] = nGet;
		}
	}

	int MapToBucket(int x){
		return x / nFactor;
	}

	void CountingSort(int[] data){
		int n = data.Length;
		for(int i = 0; i < bn; i++){
			C[i] = 0;
		}
		for(int i = 0; i < n; i++){
			C[MapToBucket(data[i])]++;
		}
		for(int i = 1; i < bn; i++){
			C[i] += C[i - 1];
		}
		int[] B = new int[n];
		for(int i = n - 1; i >= 0; i--){
			int b = MapToBucket(data[i]);
			B[--C[b]] = data[i];
		}
		for(int i = 0; i < n; i++){
			data[i] = B[i];
		}
	}

	void BucketSort(int[] data){
		int n = data.Length;
		CountingSort(data);
		for(int i = 0; i < bn; i++){
			int nLeft = C[i];
			int nRight = i == bn - 1 ? n - 1 : C[i + 1] - 1; // C[i + 1] - 1为i号桶最后一个元素的位置
			if(nLeft < nRight){
				InsertionSort(data, nLeft, nRight);
			}
		}
	}
}