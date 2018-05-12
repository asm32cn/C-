using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO; // TestReader, StreamReader
using System.Text.RegularExpressions;
using System.Text; // StringBuilder

class PaRegexp2016Apr02CS20 : Form {
	private TextBox tb1 = new TextBox();
	private TextBox tb2 = new TextBox();
	private Button btn1 = new Button();

	string s1 = null;

	public PaRegexp2016Apr02CS20(){
		this.Text = "PaRegexp2016Apr02CS20";
		this.ClientSize = new Size(800, 600);

		tb1.Multiline  = true;
		tb1.Width = 800;
		tb1.Height = 350;
		tb1.ScrollBars = ScrollBars.Vertical;
		tb1.Location = new Point(0, 0);
		tb2.Multiline  = true;
		tb2.Width = 800;
		tb2.Height = 180;
		tb2.ScrollBars = ScrollBars.Vertical;
		tb2.Location = new Point(0, 400);
		btn1.ClientSize = new Size(75, 35);
		btn1.Text = "正则一下";
		btn1.Location = new Point(0, 360);

		btn1.Click += new EventHandler(btn1_Click);

		s1 = Read();
		tb1.Text = s1;

		this.Controls.Add(tb1);
		this.Controls.Add(tb2);
		this.Controls.Add(btn1);
	}

	void btn1_Click(object sender, EventArgs e){
		Regex re1 = new Regex(@"(^\d+\.)([\s\S]*?)(?=^F\.)", 
			RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
		Match m = re1.Match(s1);
		int matchCount = 0;
		StringBuilder sb = new StringBuilder();
		while (m.Success) {
			sb.Append("Match"+ (++matchCount) + "\r\n");
			for (int i = 1; i <= 2; i++){
				Group g = m.Groups[i];
				//sb.Append("Group"+i+"='");
				sb.Append(g);
				/*CaptureCollection cc = g.Captures;
				for (int j = 0; j < cc.Count; j++){
					Capture c = cc[j];
					sb.Append("Capture"+j+"='" + c + "', Position="+c.Index + "\r\n");
				}*/
			}
			sb.Append("\r\n");
			m = m.NextMatch();
		}
		sb.Append("Count " + matchCount);
		tb2.Text = sb.ToString();
	}

	private string Read(){
		TextReader tr = new StreamReader(@"res\内分泌.txt");
		string s = tr.ReadToEnd();
		tr.Close();
		tr = null;
		return s;
	}

	public static void Main(string[] args){
		Application.Run(new PaRegexp2016Apr02CS20());
	}
}