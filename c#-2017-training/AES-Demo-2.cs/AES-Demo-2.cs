// AES-Demo-2.cs

using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

class AES_Demo_2{

	// public static byte[] _key1 = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
	// public static string keys = "dongbinhuiasxiny";//密钥,128位
	public static byte[] _key1 = { 0xc4, 0xfe, 0xbe, 0xb2, 0xd6, 0xc2, 0xd4, 0xb6, 0xb5, 0xad, 0xb2, 0xb4, 0xc3, 0xf7, 0xd6, 0xbe }; // 宁静致远淡泊明志
	public static string keys = "1a2s3d4f5g6h7j8k";//密钥,128位  

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
1IKPdYfXXvJ8eEuqtSvUrO1OBeuSscR/IX1fhpisxi7ynct+RV9dWj8geCi8DV5jxUyrdmxkQpsiYEtJ
T4Yk+jw3aAgGXgMxvuJbu3IaIppPA66294vgRhJ+htWtRauBAZFUhNIETNtvR7ZA2bp1mM1g/iGQ6Wfz
lI3qRwrwi0dEYblJOiR5bN//yT/U+JXuqe4JOBNbr+YS+inFTiqbHpa7MNTiRxKhrwXtjluCudlLlwCE
bcrjNGZJf9AiqXqF/4plSiGK/dRBdpgzEkfWq+z8LuinsD44ILIlWBVntyUhKRlEmzeXlVKqOa3lzZV3
6iSP/kLSQ4kYlo7uYY5CmxqdmU1ur3c7UmzK2HBVn+YFBQBclSindHm0RPq0gaQVe4YQBf9m0eCz7Qau
hgVBhKJunm6zaBEt+oLPll7q1WPB8Lr/AarkRl6kxAOIkIzW6FiP8mkhZjxO0k8ICjgRFZJRkuumX4a7
wVuKwIkh7/wWb15YJ13HeLOAkpFm/DmHEK34pdrtaXJFrrU60fLYLK+W9KMX84b3qEt/efptbnS1DcNS
pu/u2EgL3Rbus5vGMnFEsqUGMgzY+RzLoapI2QlQ9FvzBG3T59R3nNsLHT00PxoTlCNmAPT3HEUUJO+u
xB4ebgSK+E6gBAcut+oZ+BtV0MP7OUBjin8y28HKHYJKab2sqZj3av0gE8BVnjnq
AESDecrypt:
Hello aes ! 老子说:“上善若水”，“水善利万物而不争，处众人之所恶，故几于道”。
这里实际说的是做人的方法，即做人应如水，水滋润万物，但从不与万物争高下，这样的品
格才最接近道。

“厚德载物”语出《周易》：“君子以厚德载物。”意思是说，以深厚的德泽育人利物，今
多用来指以崇高的道德、博大精深的学识培育学子成才。清华大学的校训就是“自强不息，
厚德载物”。
*/