// cs-MS-GetHashCode-demo-1.cs
using System;

class CsMsGetHashCodeDemo1{
	public static void Main(string[] args){
		string[] A_strKeys = {"C", "C++", "Java", "C#", "Python", "Go", "Scala", "vb.net", "JavaScript", "PHP", "Perl", "Ruby"};

		CsMsGetHashCodeDemo1 cmhcd = new CsMsGetHashCodeDemo1();
		for(int i = 0, n = A_strKeys.Length; i < n; i++){
			uint nHash = cmhcd.MS_GetHash(A_strKeys[i]);
			Console.WriteLine("{0,-10} {1,-15} {2,12} {3,3}", i, A_strKeys[i], nHash, nHash % 33);
		}
	}

	public uint MS_GetHash(string s){
		int nHash = 0x15051505;
		int nHash2 = nHash;
		int nSizeInt = sizeof(int);
		unsafe{
			byte[] szData = System.Text.Encoding.Default.GetBytes(s);
			fixed(byte * pszData = szData){
				int * pData = (int *)pszData;
				for(int i = s.Length; i > 0; i -= nSizeInt + nSizeInt){
					nHash = (((nHash << 5) + nHash) + (nHash >> 0x1b)) ^ pData[0];
					if(i <= nSizeInt) break;
					nHash2 = (((nHash2 << 5) + nHash2) + (nHash2 >> 0x1b)) ^ pData[1];
					pData += 2;
				}
			}
		}
		return (uint)(nHash + (nHash2 * 0x5d588b65));
	}
}

// csc.exe /target:exe /unsafe cs-MS-GetHashCode-demo-1.cs
/*
0          C                 3452614621  16
1          C++               3450839261  23
2          Java              3975468774  24
3          C#                3452606685   0
4          Python            3793951455  30
5          Go                3452624089  13
6          Scala             4273213458  30
7          vb.net            2979897179  17
8          JavaScript         855658811  29
9          PHP               3457875952  25
10         Perl              4059615984   6
11         Ruby              3840459502  22
*/