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
		this.Text = "�ҵĵ�һ�� C# ����Ӧ��";
		//this.Width = 600;
		//this.Height = 450;
		//this.ClientSize = new Size(600, 450);
		this.BackColor = System.Drawing.Color.Black;
		this.StartPosition = FormStartPosition.CenterScreen;

		Label lblShow = new Label();
		lblShow.Location = new Point(50, 60);
		lblShow.ForeColor = System.Drawing.Color.White;
		lblShow.AutoSize = true;
		lblShow.Text = "lbl����";
		this.Controls.Add(lblShow);
		this.Paint += new PaintEventHandler(this.Form1_Paint);
		//InitializeComponent();
	}
	private void Form1_Paint(object sender, PaintEventArgs e){
		Graphics g = e.Graphics; // �������壬����Ļ�������Form�ṩ�ġ�
		Pen p = new Pen(Color.Blue, 2);//������һ����ɫ,���Ϊ�Ļ���
		g.DrawLine(p, 10, 10, 100, 100);//�ڻ����ϻ�ֱ��,��ʼ����Ϊ(10,10),�յ�����Ϊ(100,100)
		g.DrawRectangle(p, 10, 10, 100, 100);//�ڻ����ϻ�����,��ʼ����Ϊ(10,10),��Ϊ90��Ϊ90
		g.DrawEllipse(p, 10, 10, 100, 100);//�ڻ����ϻ���Բ,��ʼ����Ϊ(10,10),��Ӿ��εĿ�Ϊ,��Ϊ

	}

	[STAThreadAttribute]
	public static void Main(string[] args){
		Application.Run(new Form1());
	}
	
}