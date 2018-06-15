// cs-FNVHash1-demo-1.cs
using System;

class CsFNVHash1Demo1{
	public static void Main(string[] args){
		string[] A_strKeys = {"C", "C++", "Java", "C#", "Python", "Go", "Scala", "vb.net", "JavaScript", "PHP", "Perl", "Ruby"};

		CsFNVHash1Demo1 cfhd = new CsFNVHash1Demo1();

		for(int i = 0, n = A_strKeys.Length; i < n; i++){
			uint nHash = cfhd.FNVHash1(A_strKeys[i]);
			Console.WriteLine("{0,-10} {1,-15} {2,12} {3,3}", i, A_strKeys[i], nHash, nHash % 33);
		}
	}

	public uint FNVHash1(string s){
		uint p = 1677619;
		uint nHash = 2166136261;
		for(int i = 0, n = s.Length; i < n; i++){
			nHash = (nHash ^ s[i]) * p;
		}
		nHash += nHash << 13;
		nHash ^= nHash >> 7;
		nHash += nHash << 3;
		nHash ^= nHash >> 17;
		nHash += nHash << 5;
		return nHash;
	}
}

/*
0          C                 2196503546  17
1          C++               2331617826  21
2          Java                 8810697  27
3          C#                3069945261   9
4          Python            2130055597  10
5          Go                3741730183   4
6          Scala             1222329954  21
7          vb.net            3093588254  20
8          JavaScript          85664180   8
9          PHP               1137923497   7
10         Perl              2787627348   0
11         Ruby              3352852179   6
*/