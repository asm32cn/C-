//PaTypePad2017CS20.cs  2017-07-31

using System;
using System.Drawing;
using System.Windows.Forms;

public class PaTypePad2017CS20 : Form{

	private static TextBox tb = new TextBox();

	protected override Size DefaultSize{
		get{
			return new Size(600, 450);
		}
	}

	public PaTypePad2017CS20(){
		this.Text = "PaTypePad2017CS20.cs";
		this.StartPosition = FormStartPosition.CenterScreen;
		this.TopMost = true;

		tb.Multiline = true;
		tb.Dock = DockStyle.Fill;
		tb.TabStop = false;
		tb.BackColor = Color.FromArgb(165, 203, 247);
		tb.ScrollBars = ScrollBars.Vertical;

		this.Controls.Add( tb );
	}

	public static void Main(string[] args){
		Application.Run( new PaTypePad2017CS20 () );
	}
}
