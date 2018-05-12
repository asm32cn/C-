// web-client-demo-1.cs

using System;
using System.Net;
using System.Windows.Forms;

class web_client_demo_1{
	public web_client_demo_1(){
		//MessageBox.Show("web-client-demo-1.cs");
		MessageBox.Show( DownloadString("http://localhost/") );
	}

	public string DownloadString(string uri){
		string strContent = null;
		try{
			WebClient wc = new WebClient();
			strContent = System.Text.Encoding.UTF8.GetString( wc.DownloadData( uri ) );
		}catch(Exception ex){
			MessageBox.Show( "Exception: " + ex.Message );
		}
		return strContent;
	}

	public static void Main(string[] args){
		new web_client_demo_1();
	}
}