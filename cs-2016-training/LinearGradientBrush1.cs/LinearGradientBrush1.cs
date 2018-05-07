// LinearGradientBrush1.cs
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

class LinearGradientBrush1 : Form{

	protected override Size DefaultSize{
		get{
			return new Size(600, 450);
		}
	}

	public LinearGradientBrush1(){
		this.Text = "LinearGradientBrush1.cs";
		this.StartPosition = FormStartPosition.CenterScreen;

		this.Paint += new PaintEventHandler(Form1_Paint);
	}

	private void Form1_Paint(object sender, PaintEventArgs e) {
		Graphics g = e.Graphics;
		g.Clear(Color.White);

		//定义渐变颜色数组
		Color[] colors = {
			Color.Red,
			Color.Green,
			Color.Blue
		};

		float[] positions = {
			0.0f,
			0.3f,
			1.0f
		};

		//定义ColorBlend对象
		ColorBlend colorBlend = new ColorBlend(3);
		colorBlend.Colors = colors;
		colorBlend.Positions = positions;

		//定义线型渐变画刷
		using (LinearGradientBrush lBrush = new LinearGradientBrush(ClientRectangle, Color.White, Color.Black, LinearGradientMode.Horizontal)) {

			//设置渐变画刷的多色渐变信息
			lBrush.InterpolationColors = colorBlend;

			g.FillRectangle(lBrush, ClientRectangle);
		}
	}
	public static void Main(string[] argv){
		Application.Run(new LinearGradientBrush1());
	}
}