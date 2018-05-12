using System;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;

public class PaStarry{
	public Rectangle rect;
	public Color color;
};

public class PaStarry2016CS20 : Form {
	private ResourceManager rm = new ResourceManager("PaStarry2016CS20",
		System.Reflection.Assembly.GetExecutingAssembly());
	private Random rand;
	public PaStarry[] A_objStarry = new PaStarry[100];
	private int nCount = 100;
	private System.Threading.Timer timer;
	private int nSelected = 0;
	private int nClientWidth;
	private int nClientHeight;
	private SolidBrush brush;

	protected override Size DefaultSize {
		get{ // Set the default size of the foem to 600,450 pixels rectangle.
			return new Size(600, 450);

		}
	}

	public PaStarry2016CS20(){
		this.Text = "PaStarry2016CS20";
		//this.ClientSize = new Size(600, 400);
		this.BackColor = System.Drawing.Color.Black;
		this.StartPosition = FormStartPosition.CenterScreen;
		this.DoubleBuffered=true;
		this.Icon = (Icon)rm.GetObject("this.ico");

		rand=new Random(unchecked((int)DateTime.Now.Ticks));

		brush = new SolidBrush(System.Drawing.Color.White);

		for(int i=0; i<nCount; i++){
			A_objStarry[i] = new PaStarry();
		}

		PA_DoStarryInit();

		timer = new System.Threading.Timer(PaStarry2016CS20_Timer, null, 0, 50);

		this.Paint += new PaintEventHandler(PaStarry2016CS20_Paint);
		this.Resize += new EventHandler(PaStarry2016CS20_Resize);
	}

	public void PaStarry2016CS20_Paint(object sender, PaintEventArgs e){
		Graphics g = e.Graphics;
		for(int i=0; i<nCount; i++){
			brush.Color = A_objStarry[i].color;
			g.FillEllipse(brush, A_objStarry[i].rect);
		}
	}

	public void PaStarry2016CS20_Resize(object sender, EventArgs e){
		PA_DoStarryInit();
		this.Invalidate();
	}

	public void PaStarry2016CS20_Timer(object sender){
		nSelected = (nSelected + 1) % nCount;
		PA_DoStarrySetItem(nSelected);
		this.Invalidate();
	}


	public void PA_DoStarrySetItem(int i){
		int r = rand.Next(2, 6);

		/*
		A_objStarry[i].rect.X = rand.Next(nClientWidth);
		A_objStarry[i].rect.Y = rand.Next(nClientHeight);
		A_objStarry[i].rect.Width = r;
		A_objStarry[i].rect.Height = r;
		*/

		A_objStarry[i].rect = new Rectangle(rand.Next(nClientWidth), rand.Next(nClientHeight), r, r);
		A_objStarry[i].color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
	}

	public void PA_DoStarryInit(){
		nClientWidth = ClientRectangle.Width;
		nClientHeight = ClientRectangle.Height;
		for(int i=0; i<nCount; i++){
			PA_DoStarrySetItem(i);
		}
	}

	public static void Main(string[] args){
		Application.Run(new PaStarry2016CS20());
	}
}