using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO; // TestReader, StreamReader
using System.Text.RegularExpressions;
using System.Text; // StringBuilder

class PaDoListBookChapter2016CS : Form{
	private TextBox textbox1 = new TextBox();
	private Button button1 = new Button();
	private string s1 = null;
	protected override Size DefaultSize{
		get{
			return new Size(600, 450);
		}
	}

	public PaDoListBookChapter2016CS(){
		this.Text = "PaDoListBookChapter2016CS";
		this.StartPosition = FormStartPosition.CenterScreen;

		button1.Size = new Size(75, 24);
		button1.Location = new Point(5, 10);
		button1.Text = "Gernate";

		textbox1.Multiline  = true;
		textbox1.Width = ClientRectangle.Width;
		textbox1.Height = ClientRectangle.Height - 40;
		textbox1.ScrollBars = ScrollBars.Vertical;
		textbox1.Location = new Point(0, 40);

		this.Controls.Add(button1);
		this.Controls.Add(textbox1);

		s1 = Read();

		button1.Click +=new EventHandler(this.Button1_Click);
		this.Resize += new EventHandler(PaDoListBookChapter2016CS_Resize);
	}

	private void PaDoListBookChapter2016CS_Resize(object sender, EventArgs e){
		if(ClientRectangle.Width>0 && ClientRectangle.Height>0){
			textbox1.Size = new Size(ClientRectangle.Width, ClientRectangle.Height - 40);
		}
	}

	private void Button1_Click(object sender, EventArgs e){
		Regex re1 = new Regex(@"^第(.*)章([ ]*)(.*)$", 
			RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
		Match m = re1.Match(s1);
		int matchCount = 0;
		StringBuilder sb = new StringBuilder();
		sb.Append("Size:").Append(s1.Length.ToString()).Append("\r\n");
		while (m.Success) {
			//sb.Append("Match"+ (++matchCount) + "\r\n");
			sb.Append("第").Append(_ConvertDigit(m.Groups[1])).Append("章 ").Append(m.Groups[3]);
			/*
			for (int i = 1; i <= 2; i++){
				Group g = m.Groups[i];
				//sb.Append("Group"+i+"='");
				if(i==1){
					sb.Append(_ConvertDigit(g));
				}else{
					sb.Append(g);
				}
			}*/
			sb.Append("\r\n");
			//sb.Append(m.Groups[0]).Append("\r\n");
			matchCount++;
			m = m.NextMatch();
		}
		sb.Append("Count " + matchCount);
		textbox1.Text = sb.ToString();
	}

	static string strDigit = "一二三四五六七八九十百千";
	private int _ConvertDigit(object o){
		int n1 = 0;
		string s = o.ToString();
		if(s[0].Equals(strDigit[9])){
			n1 = 10;
		}
		for(int i=0; i<s.Length; i++){
			for(int n=0; n<9; n++){
				if(s[i].Equals(strDigit[n])){
					int n2 = n + 1;
					char char1 = (i+1<s.Length ? s[i+1] : (char)0);
					if(char1.Equals(strDigit[9])){
						n1 = n1 + n2 * 10;
					}else if(char1.Equals(strDigit[10])){
						n1 = n1 + n2 * 100;
					}else if(char1.Equals(strDigit[11])){
						n1 = n1 + n2 * 1000;
					}else{
						n1 = n1 + n2;
					}
					break;
				}
			}
		}
		return n1;
	}

	private string _BytesToString(byte[] bytes1){
		return System.Text.Encoding.Default.GetString(bytes1);
	}

	private string Read(){
		TextReader tr = new StreamReader(@"E:\My Documents\dl\医道天下txt全集(陈烨).txt", Encoding.GetEncoding("GB18030"));
		string s = tr.ReadToEnd();
		tr.Close();
		tr = null;
		return s;
	}


	public static void Main(string[] args){
		Application.Run(new PaDoListBookChapter2016CS());
	}
}