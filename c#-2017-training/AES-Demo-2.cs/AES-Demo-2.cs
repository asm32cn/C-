// AES-Demo-2.cs

using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

class AES_Demo_2{

	// public static byte[] _key1 = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
	// public static string keys = "dongbinhuiasxiny";//密钥,128位
	public static byte[] _key1 = { 0xc4, 0xfe, 0xbe, 0xb2, 0xd6, 0xc2, 0xd4, 0xb6, 0xb5, 0xad, 0xb2, 0xb4, 0xc3, 0xf7, 0xd6, 0xbe }; // 宁静致远淡泊明志
	public static string keys = "0994-18194882682";//密钥,128位  

	public static byte[] AESEncrypt(string plainText){
			SymmetricAlgorithm des = Rijndael.Create();
			byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);		
			des.Key = Encoding.UTF8.GetBytes(keys);
			des.IV = _key1;

			ICryptoTransform cTransform = des.CreateEncryptor();
			return cTransform.TransformFinalBlock(inputByteArray, 0, inputByteArray.Length);
	}

	public static byte[] AESDecrypt(byte[] cipherText){
			SymmetricAlgorithm des = Rijndael.Create();
			des.Key = Encoding.UTF8.GetBytes(keys);
			des.IV = _key1;

			ICryptoTransform cTransform = des.CreateDecryptor();
			return cTransform.TransformFinalBlock(cipherText, 0, cipherText.Length);
	}

	public static void Main(string[] args){
		string strSource = "Hello aes ! 老子说:“上善若水”，“水善利万物而不争，处众人之所恶，故几于道”。这里实际说的是做人的方法，即做人应如水，水滋润万物，但从不与万物争高下，这样的品格才最接近道。\n\n“厚德载物”语出《周易》：“君子以厚德载物。”意思是说，以深厚的德泽育人利物，今多用来指以崇高的道德、博大精深的学识培育学子成才。清华大学的校训就是“自强不息，厚德载物”。";
		string strEncrypt= Convert.ToBase64String( AESEncrypt(strSource) );
		string strDecrypt = System.Text.Encoding.UTF8.GetString( AESDecrypt( Convert.FromBase64String( strEncrypt) ) );

		Console.WriteLine(strSource);
		Console.WriteLine("AESEncrypt:\n" + strEncrypt );
		Console.WriteLine("AESDecrypt:\n" + strDecrypt );
	}
}

/*
Hello aes ! 老子说:“上善若水”，“水善利万物而不争，处众人之所恶，故几于道”。
这里实际说的是做人的方法，即做人应如水，水滋润万物，但从不与万物争高下，这样的品
格才最接近道。

“厚德载物”语出《周易》：“君子以厚德载物。”意思是说，以深厚的德泽育人利物，今
多用来指以崇高的道德、博大精深的学识培育学子成才。清华大学的校训就是“自强不息，
厚德载物”。
AESEncrypt:
Kp9Dy7bj52l5n3PMLZ8HhR79uzr3Ux2IehzLVH56bEekVCz0VEphMW3zzzUKTxgEXFUH8EAtXSsI7hLE
lvGY+mvUkjeuLsrcNfRsBJ6uRVI7sBIyNfWGuJkGL0n9PA7G+TzGI8dVSd4lkgb+lXxaywMHaoHnBTVZ
98fndWMeq7qdPkrle3ly4VWEFq0+C3gZKoDTzSBXqqsvCUY6fiLVlAsTwvSN6jEbxSNUzM7vWSRyf+Br
2zWd3V6Xeb4iy7EFFP3ky5S1zilC9YkX73iriY13nFIy8HcG0raBKcAAZ7pUeCnqmMDyeOYbHgwE+HKb
nOVYrnxjUloggIR4kv30A1LNhdfNbmovh+jwWh611Os0lUsrosZpFzaOdo17LfX/q1xvL6q+qQK5VJTg
NbeOj46p1wUSB8DIZ5RzETwY1037uEg7OVTCftvM25Aeb0s+H6HqCSQBvgLWHz0MfU3HVSHdy6dJEr+o
IZJewOjeeWJcnJEbrt2soiibqB+BL7cVGXIux38HJgupVrFYwLRQUyHxcKpccR4zg6eDTqLM7r5AdRpF
j5C+oW40PqfxbaVJGoDe4u0X5kza3joqZFa8ANnFc5HJPRkXhUDr/8S0iJuNrcJl6gN8s1/kmZnyAWTm
ImdEe9+PBfk9CGAQNQe8PkkEKMc4+lo6Advdq3jS69HHYaVgfSh2gRATVf68V6ZO
AESDecrypt:
Hello aes ! 老子说:“上善若水”，“水善利万物而不争，处众人之所恶，故几于道”。
这里实际说的是做人的方法，即做人应如水，水滋润万物，但从不与万物争高下，这样的品
格才最接近道。

“厚德载物”语出《周易》：“君子以厚德载物。”意思是说，以深厚的德泽育人利物，今
多用来指以崇高的道德、博大精深的学识培育学子成才。清华大学的校训就是“自强不息，
厚德载物”。
*/