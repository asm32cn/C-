// read-text-file-demo-1.cs

using System;
using System.IO;
using System.Windows.Forms;

class read_text_file_demo_1{

	public read_text_file_demo_1(){
		MessageBox.Show( ReadTextFile("read-text-file-demo-1.cs") );
	}

	public string ReadTextFile(string strFile){
		string strContent = null;
		try{
			using(FileStream fs = new FileStream(strFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)){
				using(StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8)){
					strContent = sr.ReadToEnd();
				}
			}
		}catch(Exception ex){
			MessageBox.Show("Exception: " + ex.Message);
		}
		return strContent;
	}

	public static void Main(string[] args){
		new read_text_file_demo_1();
	}
}