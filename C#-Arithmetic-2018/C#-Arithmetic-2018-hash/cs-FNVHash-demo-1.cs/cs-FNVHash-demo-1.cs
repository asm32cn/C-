// cs-FNVHash-demo-1.cs
using System;

class CsFNVHashDemo1{
	private const int M_MASK = 31;
	private int M_SHIFT = 0;

	public static void Main(string[] args){
		string[] A_strKeys = {"C", "C++", "Java", "C#", "Python", "Go", "Scala", "vb.net", "JavaScript", "PHP", "Perl", "Ruby"};

		CsFNVHashDemo1 cfhd = new CsFNVHashDemo1();
		for(int i = 0, l = A_strKeys.Length; i < l; i++){
			uint nHash = cfhd.FNVHash(A_strKeys[i]);
			Console.WriteLine("{0,-10} {1,-15} {2,12} {3,3}", i, A_strKeys[i], nHash, nHash % 33);
		}
	}

	public uint FNVHash(string s){
		int n = s.Length;
		uint nHash = 2166136261;
		for(int i = 0; i < n; i++){
			nHash = (nHash * 16777619) ^ s[i];
		}
		if(M_SHIFT == 0){
			return nHash;
		}
		return (nHash ^ (nHash >> M_SHIFT)) & M_MASK;
	}
}

/*
0          C                   84696412  31
1          C++               2219337286   1
2          Java              1542292725   6
3          C#                1316419575   9
4          Python            4101775411  19
5          Go                1249309159  10
6          Scala             4044407073   3
7          vb.net            3096269542  25
8          JavaScript        3504591080  23
9          PHP                589789791  30
10         Perl              3397136578  22
11         Ruby              3643069621   4
*/