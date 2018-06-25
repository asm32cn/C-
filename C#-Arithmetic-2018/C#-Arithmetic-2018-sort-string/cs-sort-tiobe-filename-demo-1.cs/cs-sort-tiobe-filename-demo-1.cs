// cs-sort-tiobe-filename-demo-1.cs
// .Net 2.0

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class cs_sort_tiobe_filename_demo_1{
	private Dictionary<string, int> _month = new Dictionary<string, int>();

	private delegate int _GetValue(int i);
	private delegate int _SetValue(int i, int n);

	public cs_sort_tiobe_filename_demo_1(){
		_month.Add("January", 1);
		_month.Add("February", 2);
		_month.Add("March", 3);
		_month.Add("April", 4);
		_month.Add("May", 5);
		_month.Add("June", 6);
		_month.Add("July", 7);
		_month.Add("August", 8);
		_month.Add("September", 9);
		_month.Add("October", 10);
		_month.Add("November", 11);
		_month.Add("December", 12);
	}

	public static void Main(string[] args){
		string[] A_strFiles = {
			"TIOBE Index for April 2018.html",
			"TIOBE Index for February 2018.html",
			"TIOBE Index for January 2018.html",
			"TIOBE Index for June 2018.html",
			"TIOBE Index for March 2018.html",
			"TIOBE Index for May 2018.html",
			"TIOBE-exchange-matrix-data.html",
			"TIOBE-exchange-matrix-data.py",
			"TIOBE-gernate-index-py2.py",
			"TIOBE-index.html",
			"TIOBE_matrixData.txt"};

		cs_sort_tiobe_filename_demo_1 stfd = new cs_sort_tiobe_filename_demo_1();

		string[] sorted = stfd.sort_tiobe_filename_demo(A_strFiles);

		string strFormat = "{0,-2} {1,-36} {2,-36}";
		Console.WriteLine(string.Format(strFormat, "@", "Source data", "Sorted data"));
		Console.WriteLine(string.Format(strFormat, "==", "==================", "=================="));
		for(int i = 0, l = A_strFiles.Length; i < l; i++){ 
			Console.WriteLine(string.Format(strFormat, i, A_strFiles[i], sorted[i]));
		}
	}

	public string[] sort_tiobe_filename_demo(string[] files){
		int nCount = files.Length;
		string[] sorted = new string[nCount];
		
		// init buff
		int[,] buff = new int[11, 3];
		int n = 0;
		Regex regex = new Regex("^TIOBE Index for (\\w+) (\\d{4})\\.html$");
		for(int i = 0; i < nCount; i++){
			buff[i, 0] = buff[i, 1] = i;
			MatchCollection ms = regex.Matches(files[i]);
			int nDatespan = 0;
			if(ms.Count == 1 && ms[0].Groups.Count == 3){
				int nMonth = 0;
				_month.TryGetValue(ms[0].Groups[1].ToString(), out nMonth);
				nDatespan = Convert.ToInt32(ms[0].Groups[2].ToString()) * 100 + nMonth;
				// Console.WriteLine(ms[0].Groups[2] + ", " + ms[0].Groups[1]);
			}else{
				nDatespan = ++n;
			}
			buff[i, 2] = nDatespan;
			// Console.WriteLine(buff[i, 0] + " " + buff[i, 1] + " " + buff[i, 2].ToString());
		}


		// int c = 1;
		// Action<int> add = delegate(int i){ c += i; Console.WriteLine("c add " + i + " = " + c); };
		// add(2); // c add 2 = 3

		_GetValue GetIndex = delegate(int i) { return buff[i, 1]; };
		_SetValue SetIndex = delegate(int i, int nn) { return (buff[i, 1] = nn); };
		_GetValue GetData = delegate (int i) { return buff[GetIndex(i), 2]; };

		// insertion sort string
		for(int i = 0; i < nCount - 1; i++){
			int nMin = i;
			for(int j = i + 1; j < nCount; j++){
				if(GetData(j) < GetData(nMin)){
					nMin = j;
				}
			}
			if(i != nMin){
				int t = GetIndex(i);
				SetIndex(i, GetIndex(nMin));
				SetIndex(nMin, t);
			}
		}
		for(int i = 0; i < nCount; i++){
			// Console.WriteLine(" " + buff[i, 0] + " " + buff[i, 1] + " " + buff[i, 2] + " ");
			sorted[i] = files[GetIndex(i)];
		}
		return sorted;
	}
}