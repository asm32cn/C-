// cs-RSHash-demo-1.cs
using System;

class CsRSHashDemo1{
	public static void Main(string[] args){
		string[] A_strKeys = {"C", "C++", "Java", "C#", "Python", "Go", "Scala", "vb.net", "JavaScript", "PHP", "Perl", "Ruby"};

		CsRSHashDemo1 crhd = new CsRSHashDemo1();
		for(int i = 0, n = A_strKeys.Length; i < n; i++){
			uint nHash = crhd.RSHash(A_strKeys[i]);
			Console.WriteLine("{0,-10} {1,-15} {2,12} {3,3}", i, A_strKeys[i], nHash, nHash % 31);
		}
	}

	public uint RSHash(string s){
		uint a = 63689;
		uint b = 378551;
		uint nHash = 0;
		for(int i = 0, n = s.Length; i < n; i++){
			nHash = nHash * a + s[i];
			a *= b;
		}
		return nHash & 0x7fffffff;
	}
}

/*
0          C                         67   5
1          C++               1535458403   7
2          Java              1675714860   2
3          C#                 431117552  25
4          Python             481257876  19
5          Go                 232492024  22
6          Scala             1047644580  14
7          vb.net             550038443  18
8          JavaScript         711855001  17
9          PHP               1664999048  22
10         Perl               340008717   4
11         Ruby               677893036   9
*/