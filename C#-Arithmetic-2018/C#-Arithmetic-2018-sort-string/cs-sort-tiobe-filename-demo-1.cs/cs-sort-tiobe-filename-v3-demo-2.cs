// cs-sort-tiobe-filename-v3-demo-2.cs
// .Net 3.0

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class cs_sort_tiobe_filename_demo_1{
	private Dictionary<string, int> _month = new Dictionary<string, int>{{"January", 1}, {"February", 2}, {"March", 3}, {"April", 4},
		{"May", 5}, {"June", 6}, {"July", 7}, {"August", 8}, {"September", 9}, {"October", 10}, {"November", 11}, {"December", 12}};

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
		}

		Func<int, int> GetIndex = (i) => { return buff[i, 1]; };
		Func<int, int, int> SetIndex = (i, nn) => { return(buff[i, 1] = nn); };
		Func<int, int> GetData = (i) => { return buff[GetIndex(i), 2]; };

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

/*
@  Source data                          Sorted data
== ==================                   ==================
0  TIOBE Index for April 2018.html      TIOBE-exchange-matrix-data.html
1  TIOBE Index for February 2018.html   TIOBE-exchange-matrix-data.py
2  TIOBE Index for January 2018.html    TIOBE-gernate-index-py2.py
3  TIOBE Index for June 2018.html       TIOBE-index.html
4  TIOBE Index for March 2018.html      TIOBE_matrixData.txt
5  TIOBE Index for May 2018.html        TIOBE Index for January 2018.html
6  TIOBE-exchange-matrix-data.html      TIOBE Index for February 2018.html
7  TIOBE-exchange-matrix-data.py        TIOBE Index for March 2018.html
8  TIOBE-gernate-index-py2.py           TIOBE Index for April 2018.html
9  TIOBE-index.html                     TIOBE Index for May 2018.html
10 TIOBE_matrixData.txt                 TIOBE Index for June 2018.html
*/