// cs-sort-tiobe-filename-demo-1.cs
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class cs_sort_tiobe_filename_demo_1{
	private Dictionary<string, int> _month = new Dictionary<string, int>();//{{"January", 1}, {"February", 2}, {"March", 3}, {"April", 4},
		//{"May", 5}, {"June", 6}, {"July", 7}, {"August", 8}, {"September", 9}, {"October", 10}, {"November", 11}, {"December", 12}};

	public cs_sort_tiobe_filename_demo_1(){
		_month.Add("January", 1);
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

		Console.WriteLine(A_strFiles.Length);
	}

	public string[] sort_tiobe_filename_demo(string[] files){
		int nCount = files.Length;
		string[] sorted = new string[nCount];
		int[,] buff = new int[11, 3];
		int n = 0;
		Regex regex = new Regex("^TIOBE Index for (\\w+) (\\d{4})\\.html$");
		for(int i = 0; i < nCount; i++){
			buff[i, 0] = buff[i, 1] = i;
			MatchCollection ms = regex.Matches(files[i]);
			int nDatespan = 0;
			if(ms.Count == 1 && ms[0].Groups.Count == 3){
				nDatespan = Convert.ToInt32(ms[0].Groups[2].ToString()) * 100;// + _month[ms[0].Groups[1].ToString()];
				Console.WriteLine(ms[0].Groups[2] + ", " + ms[0].Groups[1]);
				// Console.WriteLine();
			}else{
				nDatespan = ++n;
			}
			buff[i, 2] = nDatespan;
			Console.WriteLine(buff[i, 0] + " " + buff[i, 1] + " " + buff[i, 2].ToString());
		}
		return sorted;
	}
}