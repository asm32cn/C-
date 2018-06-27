// AES-Demo-1.cs


/*
C#实现的AES加密解密完整实例

本文实例讲述了C#实现的AES加密解密。分享给大家供大家参考，具体如下：
*/


/******************************************************************
* 创建人：HTL
* 说明：C# AES加密解密
*******************************************************************/

using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

public class AES_Demo_1{

	public static void Main(){
		//密码
		// string password="123456789012345678901234567890123456789012345678";
		string password="abc def ghi jkl ";
		//加密初始化向量
		string iv="0994-18194882682";

		// IV的字节数必须等于SymmetricAlgorithm.BlockSize/8
		Console.WriteLine("SymmetricAlgorithm.BlockSize/8 = " + 16);

		string message=AESEncrypt("12345", password, iv);
		Console.WriteLine(message);

		string message2 = AESDecrypt(message, password, iv);
		// message=AESDecrypt("8Z3dZzqn05FmiuBLowExK0CAbs4TY2GorC2dDPVlsn/tP+VuJGePqIMv1uSaVErr",password,iv);
		Console.WriteLine(message2);
	}

	/// <summary>
	/// AES加密
	/// </summary>
	/// <param name="text">加密字符</param>
	/// <param name="password">加密的密码</param>
	/// <param name="iv">密钥</param>
	/// <returns></returns>
	public static string AESEncrypt(string text, string password, string iv){
		RijndaelManaged rijndaelCipher = new RijndaelManaged();
		rijndaelCipher.Mode = CipherMode.CBC;
		rijndaelCipher.Padding = PaddingMode.PKCS7;
		rijndaelCipher.KeySize = 128;
		rijndaelCipher.BlockSize = 128;
		byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(password);
		byte[] keyBytes = new byte[16];
		int len = pwdBytes.Length;
		if (len > keyBytes.Length) len = keyBytes.Length;
		System.Array.Copy(pwdBytes, keyBytes, len);
		rijndaelCipher.Key = keyBytes;
		byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(iv);
		rijndaelCipher.IV = new byte[16];
		ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
		byte[] plainText = Encoding.UTF8.GetBytes(text);
		byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
		return Convert.ToBase64String(cipherBytes);
	}

	/// <summary>
	/// AES解密
	/// </summary>
	/// <param name="text"></param>
	/// <param name="password"></param>
	/// <param name="iv"></param>
	/// <returns></returns>
	public static string AESDecrypt(string text, string password, string iv){
		string strContent = null;
		try{
			RijndaelManaged rijndaelCipher = new RijndaelManaged();
			rijndaelCipher.Mode = CipherMode.CBC;
			rijndaelCipher.Padding = PaddingMode.PKCS7;
			rijndaelCipher.KeySize = 128;
			rijndaelCipher.BlockSize = 128;
			byte[] encryptedData = Convert.FromBase64String(text);
			byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(password);
			byte[] keyBytes = new byte[16];
			int len = pwdBytes.Length;
			if (len > keyBytes.Length) len = keyBytes.Length;
			System.Array.Copy(pwdBytes, keyBytes, len);
			rijndaelCipher.Key = keyBytes;
			byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(iv);
			rijndaelCipher.IV = ivBytes;
			ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
			byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
			strContent = Encoding.UTF8.GetString(plainText);
		}catch(Exception ex){
			Console.WriteLine("Exception: " + ex.Message);
		}
		return strContent;
	}
}