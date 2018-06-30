// cs-base64-demo-1.cs
using System;
using System.IO;
using System.Text;

class cs_base64_demo_1{
	public const string _keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
	public static void Main(string[] args){
		cs_base64_demo_1 cbd = new cs_base64_demo_1();

		string strSource = "cs-base64-demo-1.cs";
		byte[] data = Encoding.UTF8.GetBytes(strSource);

		Console.WriteLine(cbd.Base64Encoder(data));
		Console.WriteLine(cbd.Base64Encoder1(data));
		Console.WriteLine(strSource);
	}

	public string Base64Encoder1(byte[] data){
		int nCount = data.Length;
		StringBuilder sb = new StringBuilder();
		// sb.Append(nCount);
		int i = 0;
		byte b1, b2, b3;
		while(i < nCount){
			b1 = data[i++];
			if(i == nCount){
				sb.Append( _keyStr[b1 >> 2] );
				sb.Append( _keyStr[(b1 & 0x3 ) << 4] );
				sb.Append("==");
				break;
			}
			b2 = data[i++];
			if(i == nCount){
				sb.Append( _keyStr[(b1 >> 2)] );
				sb.Append( _keyStr[(b1 & 0x3 ) << 4] );
				sb.Append( _keyStr[(b2 & 0xf ) << 2] );
				sb.Append("=");
				break;
			}
			b3 = data[i++];
			sb.Append( _keyStr[b1 >> 2] );
			sb.Append( _keyStr[((b1 & 0x3) << 4) | ((b2 & 0xf0) >> 4)] );
			sb.Append( _keyStr[((b2 & 0xf) << 2) | ((b3 & 0xc0) >> 6)] );
			sb.Append( _keyStr[b3 & 0x3f] );
		}
		return sb.ToString();
	}

	public string Base64Encoder(byte[] data){
		StringBuilder sb = new StringBuilder();
		byte chr1 = 0, chr2 = 0, chr3 = 0;
		byte enc1 = 0, enc2 = 0, enc3 = 0, enc4 = 0;
		int i = 0, nCount = data.Length;
		while(i < nCount){
			chr1 = data[i++];
			enc1 = (byte)(chr1 >> 2);
			if(i < nCount){
				chr2 = data[i++];
				if(i < nCount){
					chr3 = data[i++];
					enc2 = (byte)(((chr1 & 3) << 4) | (chr2 >> 4));
					if(i < nCount){
						enc3 = (byte)(((chr2 & 15) << 2) | (chr3 >> 6));
						enc4 = (byte)(chr3 & 63);
					}
				}else{
					enc3 = (byte)((chr2 & 15) << 2);
					enc4 = 64;
				}
			}else{
				enc2 = (byte)((chr1 & 3) << 4);
				enc3 = enc4 = 64;
			}
			sb.Append(_keyStr[enc1]).Append(_keyStr[enc2]);
			sb.Append(_keyStr[enc3]).Append(_keyStr[enc4]);
		}
		return sb.ToString();
	}
}

/*
19
Y3MtYmFzZTY0LWRlbW8tMS5jcw==
Y3MtYmFzZTY0LWRlbW8tMS5jcw==
cs-base64-demo-1.cs
*/