// cs-base64-demo-1.cs
using System;
using System.IO;
using System.Text;
using System.Collections;

class cs_base64_demo_1{
	public const string _keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
	public static void Main(string[] args){
		cs_base64_demo_1 cbd = new cs_base64_demo_1();

		string strSource = "cs-base64-demo-1.cs\n程序中书写着所见所闻所感，编译着心中的万水千山。";
		byte[] data = Encoding.UTF8.GetBytes(strSource);
		string strEncoded = cbd.Base64Encoder(data);
		// string strEncoded = Convert.ToBase64String(data);
		byte[] dec = cbd.Base64Decoder(strEncoded);

		Console.WriteLine("nCount = " + strSource.Length);
		Console.WriteLine(strSource);
		Console.WriteLine("data nCount = " + data.Length);
		// Console.WriteLine(strEncoded);
		Console.WriteLine("Base64Encoder:\n" + cbd.Base64Encoder(data));
		Console.WriteLine("Base64Encoder2:\n" + cbd.Base64Encoder2(data));
		Console.WriteLine("Convert.ToBase64String:\n" + Convert.ToBase64String(data));
		Console.WriteLine("Base64String nCount = " + strEncoded.Length);
		Console.WriteLine( Encoding.UTF8.GetString(dec) );
		Console.WriteLine( Encoding.UTF8.GetString(Convert.FromBase64String(strEncoded)) );
	}

	public string Base64Encoder2(byte[] data){
		int nCount = data.Length;
		StringBuilder sb = new StringBuilder();
		int i = 0;
		byte b1, b2, b3;
		while(i < nCount){
			b1 = data[i++];
			sb.Append( _keyStr[b1 >> 2] );
			if(i == nCount){
				sb.Append( _keyStr[(b1 & 0x3 ) << 4] );
				sb.Append("==");
				break;
			}
			b2 = data[i++];
			sb.Append( _keyStr[((b1 & 0x3 ) << 4) | ((b2 & 0xf0) >> 4)] );
			if(i == nCount){
				sb.Append( _keyStr[(b2 & 0xf ) << 2] );
				sb.Append("=");
				break;
			}
			b3 = data[i++];
			sb.Append( _keyStr[((b2 & 0xf) << 2) | ((b3 & 0xc0) >> 6)] );
			sb.Append( _keyStr[b3 & 0x3f] );
		}
		return sb.ToString();
	}

	public string Base64Encoder(byte[] data){
		StringBuilder sb = new StringBuilder();
		byte chr1 = 0, chr2 = 0, chr3 = 0;
		int enc1 = 0, enc2 = 0, enc3 = 0, enc4 = 0;
		int i = 0, nCount = data.Length;
		while(i < nCount){
			chr1 = data[i++];
			enc1 = chr1 >> 2;
			if(i < nCount){
				chr2 = data[i++];
				enc2 = ((chr1 & 3) << 4) | ((chr2 & 0xf0/*240*/) >> 4);
				if(i < nCount){
					chr3 = data[i++];
					enc3 = ((chr2 & 15) << 2) | ((chr3 & 0xc0/*192*/) >> 6);
					enc4 = chr3 & 63;
				}else{
					enc3 = (chr2 & 15) << 2;
					enc4 = 64;
				}
			}else{
				enc2 = (chr1 & 3) << 4;
				enc3 = enc4 = 64;
			}
			sb.Append(_keyStr[enc1]).Append(_keyStr[enc2]);
			sb.Append(_keyStr[enc3]).Append(_keyStr[enc4]);
		}
		return sb.ToString();
	}

	private Hashtable ht = new Hashtable();
	public void InitHashTable(){
		ht.Clear();
		for(int i = 0, l = _keyStr.Length; i < l; i++)
			ht.Add(_keyStr[i], i);
	}

	public byte[] Base64Decoder(string s){
		InitHashTable();
		byte chr1 = 0, chr2 = 0, chr3 = 0;
		int enc1 = 0, enc2 = 0, enc3 = 0, enc4 = 0;
		int i = 0, n = 0, nCount = s.Length;
		byte[] data = new byte[nCount * 3 / 4];
		while(i < nCount){
			enc1 = (int)ht[s[i++]];
			enc2 = (int)ht[s[i++]];
			enc3 = (int)ht[s[i++]];
			enc4 = (int)ht[s[i++]];

			chr1 = (byte)((enc1 << 2) | (enc2 >> 4));
			chr2 = (byte)(((enc2 & 15) << 4) | (enc3 >> 2));
			chr3 = (byte)(((enc3 & 3) << 6) | enc4);
			data[n++] = chr1;
			if(enc3 != 64) data[n++] = chr2;
			if(enc4 != 64) data[n++] = chr3;
		}
		return data;
	}
}

/*
nCount = 44
cs-base64-demo-1.cs
程序中书写着所见所闻所感，编译着心中的万水千山。
data nCount = 92
Base64Encoder:
Y3MtYmFzZTY0LWRlbW8tMS5jcwrnqIvluo/kuK3kuablhpnnnYDmiYDop4HmiYDpl7vmiYDmhJ/vvIznvJbor5HnnYDlv4PkuK3nmoTkuIfmsLTljYPlsbHjgII=
Base64Encoder2:
Y3MtYmFzZTY0LWRlbW8tMS5jcwrnqIvluo/kuK3kuablhpnnnYDmiYDop4HmiYDpl7vmiYDmhJ/vvIznvJbor5HnnYDlv4PkuK3nmoTkuIfmsLTljYPlsbHjgII=
Convert.ToBase64String:
Y3MtYmFzZTY0LWRlbW8tMS5jcwrnqIvluo/kuK3kuablhpnnnYDmiYDop4HmiYDpl7vmiYDmhJ/vvIznvJbor5HnnYDlv4PkuK3nmoTkuIfmsLTljYPlsbHjgII=
Base64String nCount = 124
cs-base64-demo-1.cs
程序中书写着所见所闻所感，编译着心中的万水千山。
cs-base64-demo-1.cs
程序中书写着所见所闻所感，编译着心中的万水千山。
*/