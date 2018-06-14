// cs-additive-hash-demo-1.cs

using System;

class CsAdditiveHashDemo1{
	public static void Main(string[] args){
		string[] A_strKeys = {"C", "C++", "Java", "C#", "Python", "Go", "Scala", "vb.net", "JavaScript", "PHP", "Perl", "Ruby"};
		CsAdditiveHashDemo1 cahd = new CsAdditiveHashDemo1();

		for(int i = 0, n = A_strKeys.Length; i < n; i++){
			Console.WriteLine(string.Format("{0,-10} {1,-15} {2,3}", i, A_strKeys[i], cahd.AdditiveHash(A_strKeys[i], 31)));
		}
	}

	public uint AdditiveHash(string s, uint nPrime){
		uint nHash, n = (uint)s.Length ;
		int i;
		for(nHash = n, i = 0; i < n; i++){
			nHash += s[i];
		}
		return nHash % nPrime;
	}
}

/*
0          C                 6
1          C++               1
2          Java             18
3          C#               11
4          Python           28
5          Go               29
6          Scala            24
7          vb.net            6
8          JavaScript        2
9          PHP              18
10         Perl              4
11         Ruby             19
*/