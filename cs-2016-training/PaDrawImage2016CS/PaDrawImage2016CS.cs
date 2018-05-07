using System;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;

public class PaDrawImage2016CS : Form{
	public PaDrawImage2016CS(){
		this.Text = "PaDrawImage2016CS";
		this.Width = 600;
		this.Height = 400;
		this.BackColor = System.Drawing.Color.Black;

		this.Paint += new PaintEventHandler(PaDrawImage2016CS_Paint);
	}

	private void PaDrawImage2016CS_Paint(object sender, PaintEventArgs e){
		Graphics g = e.Graphics;
		Pen pen = new Pen(System.Drawing.Color.White, 2);
		//g.DrawEllipse(pen, 0, 0, 100, 100);
		g.DrawEllipse(pen, ClientRectangle);
	}

	public static void Main(string[] args){
		Application.Run(new PaDrawImage2016CS());
	}
}