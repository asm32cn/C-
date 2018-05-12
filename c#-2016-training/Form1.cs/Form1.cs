using System;
using System.Drawing;
using System.Windows.Forms;

public class Form1 : Form{

	protected override Size DefaultSize{
		get{
			return new Size(600, 450);
		}
	}

	public Form1(){
		this.Text = "我的第一个 C# 窗体应用";
		//this.Width = 600;
		//this.Height = 450;
		//this.ClientSize = new Size(600, 450);
		this.BackColor = System.Drawing.Color.Black;
		this.StartPosition = FormStartPosition.CenterScreen;

		Label lblShow = new Label();
		lblShow.Location = new Point(50, 60);
		lblShow.ForeColor = System.Drawing.Color.White;
		lblShow.AutoSize = true;
		lblShow.Text = "lbl测试";
		this.Controls.Add(lblShow);
		this.Paint += new PaintEventHandler(this.Form1_Paint);
		//InitializeComponent();
	}
	private void Form1_Paint(object sender, PaintEventArgs e){
		Graphics g = e.Graphics; // 创建画板，这里的画板是由Form提供的。
		Pen p = new Pen(Color.Blue, 2);//定义了一个蓝色,宽度为的画笔
		g.DrawLine(p, 10, 10, 100, 100);//在画板上画直线,起始坐标为(10,10),终点坐标为(100,100)
		g.DrawRectangle(p, 10, 10, 100, 100);//在画板上画矩形,起始坐标为(10,10),宽为90高为90
		g.DrawEllipse(p, 10, 10, 100, 100);//在画板上画椭圆,起始坐标为(10,10),外接矩形的宽为,高为

	}

	[STAThreadAttribute]
	public static void Main(string[] args){
		Application.Run(new Form1());
	}
	
}