// ButtonTextAlignDemo1.cs

using System;
using System.Drawing;
using System.Windows.Forms;

class ButtonTextAlignDemo1 : Form{
	private Button[] btnCommands = new Button[2];
	public ButtonTextAlignDemo1(){
		this.Text = "ButtonTextAlignDemo1.cs";
		this.StartPosition = FormStartPosition.CenterScreen;
		this.Padding = new Padding(10);

		for(int i = 0; i < 2; i++){
			btnCommands[i] = new Button();
			btnCommands[i].Text = string.Format("ButtonTextAlignDemo1.cs - {0}", i);
			btnCommands[i].Dock = DockStyle.Top;
			btnCommands[i].TextAlign = ContentAlignment.MiddleLeft;

			this.Controls.Add(btnCommands[i]);
		}
	}
	public static void Main(string[] args){
		Application.Run(new ButtonTextAlignDemo1());
	}
}