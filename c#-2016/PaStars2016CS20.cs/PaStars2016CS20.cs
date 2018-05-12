using System;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;

class PaStars2016CS20 : Form {
	private ResourceManager rm = new ResourceManager("PaStars2016CS20",
		System.Reflection.Assembly.GetExecutingAssembly());
	private int nClientWidth, nClientHeight;
	private const int nCount = 30;
	private Random rand = new Random();
	private SolidBrush brush1 = new SolidBrush(Color.White);
	private System.Threading.Timer timer1 = null;
	private Star2016Def[] A_objStart = new Star2016Def[nCount];
	private int nSelectedID = 0;
	
	protected override Size DefaultSize{
		get{
			return new Size(600, 450);
		}
	}

	public PaStars2016CS20(){
		this.Text = "PaStars2016CS20";
		this.BackColor = Color.Black;
		this.StartPosition = FormStartPosition.CenterScreen;
		this.DoubleBuffered = true;
		this.Icon = (Icon)rm.GetObject("this.ico");

		for(int i=0; i<nCount; i++){
			A_objStart[i] = new Star2016Def();
		}

		//this.ClientSize = new Size(600, 450);

		PA_DoFormResize();

		timer1 = new System.Threading.Timer(PaStars2016CS20_Timer, null, 0, 200);

		this.Paint += new PaintEventHandler(PaStars2016CS20_Paint);
		this.Resize += new EventHandler(PaStars2016CS20_Resize);
	}

	public void PaStars2016CS20_Timer(object sender){
		nSelectedID = (nSelectedID+1)%nCount;
		PA_DoStarInit(nSelectedID);
		PA_DoInvalidate();
	}

	private void PA_DoInvalidate(){
		this.Invalidate();
	}

	protected void PaStars2016CS20_Paint(object sender, PaintEventArgs e){
		Graphics g = e.Graphics;

		for(int n=0; n<nCount; n++){
			brush1.Color = Color.FromArgb(A_objStart[n].cr, A_objStart[n].cg, A_objStart[n].cb);
			g.FillPolygon(brush1, A_objStart[n].pts);
		}

	}

	protected void PaStars2016CS20_Resize(object sender, EventArgs e){
		if(ClientRectangle.Width>0 && ClientRectangle.Height>0){
			PA_DoFormResize();
		}
	}

	private void PA_DoFormResize(){
		nClientWidth = ClientRectangle.Width;
		nClientHeight = ClientRectangle.Height;
		for(int i=0; i<nCount; i++){
			PA_DoStarInit(i);
		}
		PA_DoInvalidate();
	}

	private void PA_DoStarInit(int i){
		int r = 20 + rand.Next(nClientWidth/20);
		A_objStart[i].init(r,
			r + rand.Next(nClientWidth-r-r),
			r + rand.Next(nClientHeight-r-r), rand);
	}

	public static void Main(string[] args){
		Application.Run(new PaStars2016CS20());
	}
}

class Star2016Def{
	public static double PI2 = Math.PI + Math.PI;
	public Point[] pts = new Point[11];
	int cx, cy, r;
	public int cr, cg, cb;

	public Star2016Def(){}

	public Star2016Def(int r, int cx, int cy, Random rand){
		init(r, cx, cy, rand);
	}

	public void init(int r, int cx, int cy, Random rand){
		this.r = r;
		this.cx = cx;
		this.cy = cy;
		int r2 = r / 2;
		double a1 = PI2 / 10;
		for(int i=0; i<5; i++){
			int id = i + i;
			double a2 = PI2 * i / 5;
			pts[id].X = cx + (int)(Math.Sin(a2) * r);
			pts[id].Y = cy - (int)(Math.Cos(a2) * r);
			a2 = PI2 * i / 5 + a1;
			pts[id+1].X = cx + (int)(Math.Sin(a2) * r2);
			pts[id+1].Y = cy - (int)(Math.Cos(a2) * r2);
		}
		pts[10].X = pts[0].X;
		pts[10].Y = pts[0].Y;
		cr = rand.Next(256);
		cg = rand.Next(256);
		cb = rand.Next(256);
	}
}
