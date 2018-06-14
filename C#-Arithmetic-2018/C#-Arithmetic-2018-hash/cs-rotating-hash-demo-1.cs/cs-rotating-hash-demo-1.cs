// cs-rotating-hash-demo-1.cs
using System;

class CsRotatingHashDemo1{
	public static void Main(string[] args){
		string[] A_strKeys = {"C", "C++", "Java", "C#", "Python", "Go", "Scala", "vb.net", "JavaScript", "PHP", "Perl", "Ruby"};

		CsRotatingHashDemo1 crhd = new CsRotatingHashDemo1();
		for(int i = 0, n = A_strKeys.Length; i < n; i++){
			Console.WriteLine(String.Format("{0,-10} {1,-15} {2,3}", i, A_strKeys[i], crhd.RotatingHash(A_strKeys[i], 31)));
		}
	}

	public uint RotatingHash(string s, uint nPrime){
		uint nHash, n = (uint)s.Length;
		int i = 0;
		for(nHash = n, i = 0; i < n; i++){
			nHash = (nHash << 4 >> 28) ^ s[i];
		}
		return nHash % nPrime;
	}
}

/*
0          C                 5
1          C++              12
2          Java              4
3          C#                4
4          Python           17
5          Go               18
6          Scala             4
7          vb.net           23
8          JavaScript       23
9          PHP              18
10         Perl             15
11         Ruby             28
*/