// cs-JSHash-demo-1.cs
using System;

class CsJSHashDemo1{
	public static void Main(string[] args){
		string[] A_strKeys = {"C", "C++", "Java", "C#", "Python", "Go", "Scala", "vb.net", "JavaScript", "PHP", "Perl", "Ruby"};

		CsJSHashDemo1 cjhd = new CsJSHashDemo1();
		for(int i = 0, l = A_strKeys.Length; i < l; i++){
			uint nHash = cjhd.JSHash(A_strKeys[i]);
			Console.WriteLine("{0,-10} {1,-15} {2,12} {3,3}", i, A_strKeys[i], nHash, nHash % 31);
		}
	}

	public uint JSHash(string s){
		uint nHash = 1315423911;
		for(int i = 0, n = s.Length; i < n; i++){
			nHash ^= (nHash << 5) + s[i] + (nHash >> 2);
		}
		return nHash & 0x7fffffff;
	}
}

/*
0          C                  787808363   0
1          C++                446398608   3
2          Java              1082641991   6
3          C#                 615009782   7
4          Python            1495460415  17
5          Go                 615010075  21
6          Scala             1696237482   4
7          vb.net              59114926   3
8          JavaScript          35438201  24
9          PHP                446451679   2
10         Perl              1085069648  22
11         Ruby              1084837418  13
*/