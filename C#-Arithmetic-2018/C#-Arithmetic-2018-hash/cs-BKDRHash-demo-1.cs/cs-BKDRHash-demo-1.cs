// cs-BKDRHash-demo-1.cs
using System;

class CsBKDRHashDemo1{
	public static void Main(string[] args){
		string[] A_strKeys = {"C", "C++", "Java", "C#", "Python", "Go", "Scala", "vb.net", "JavaScript", "PHP", "Perl", "Ruby"};

		CsBKDRHashDemo1 cbhd = new CsBKDRHashDemo1();

		for(int i = 0, n = A_strKeys.Length; i < n; i++){
			uint nHash = cbhd.BKDRHash(A_strKeys[i]);
			Console.WriteLine(string.Format("{0,-10} {1,-15} {2,-12} {3,3}", i, A_strKeys[i], nHash, nHash % 31));
		}
	}

	public uint BKDRHash(string s){
		uint nSeed = 131; // 31 131 1313 13131 131313 etc
		uint n = (uint)s.Length, nHash = 0;
		for(int i = 0; i < n; i++){
			nHash = nHash * nSeed + s[i];
		}
		return nHash & 0x7fffffff;
	}
}

/*
0          C               67             5
1          C++             1155463        0
2          Java            168038906     27
3          C#              8812           8
4          Python          1962499928     9
5          Go              9412          19
6          Scala           1045413186     0
7          vb.net          763463135      2
8          JavaScript      557701633      8
9          PHP             1382392        9
10         Perl            181595583      1
11         Ruby            186364258      8
*/