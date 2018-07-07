// ButtonTextAlignDemo1.cs

using System;
using System.Drawing;
using System.Windows.Forms;

class ButtonTextAlignDemo1 : Form{
	private Button btn = new Button();
	public ButtonTextAlignDemo1(){
		this.Text = "ButtonTextAlignDemo1.cs";
		this.StartPosition = FormStartPosition.CenterScreen;
		this.Padding = new Padding(10);

		btn.Text = "ButtonTextAlignDemo1.cs";
		btn.Dock = DockStyle.Top;
		// btn.TextAlign = CenterLeft;
		btn.TextAlign = ContentAlignment.MiddleLeft;
		this.Controls.Add(btn);
	}
	public static void Main(string[] args){
		Application.Run(new ButtonTextAlignDemo1());
	}
}