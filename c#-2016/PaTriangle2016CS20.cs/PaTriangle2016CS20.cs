using System;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;

public class PaTriangle2016CS20 : Form{
	private ResourceManager rm = new ResourceManager("PaTriangle2016CS20",
		System.Reflection.Assembly.GetExecutingAssembly());
	private PaTriangleDef pts = new PaTriangleDef();
	private PaTriangleDef pts_t;
	private int nCount = 50;
	private Pen pen;
	private int nClientWidth;
	private int nClientHeight;
	private Random rand;
	private System.Threading.Timer timer;

	protected override Size DefaultSize {
	   get{ // Set the default size of the form to 600,450 pixels rectangle.
		  return new Size(600,450);
	   }
	}

	public PaTriangle2016CS20(){
		this.Text = "PaTriangle2016CS20";
		//this.ClientSize = new Size(600, 400);
		this.BackColor = System.Drawing.Color.Black;
		this.StartPosition = FormStartPosition.CenterScreen;
		this.DoubleBuffered=true;
		this.Icon = (Icon)rm.GetObject("this.ico");

		pen = new Pen(System.Drawing.Color.White, 1);

		rand = new Random(unchecked((int)DateTime.Now.Ticks));

		PA_DoTriangleInit();

		timer = new System.Threading.Timer(PaTriangle2016CS20_Timer, null, 0, 10);

		this.Paint += new PaintEventHandler(PaTriangle2016CS20_Paint);
		this.Resize += new EventHandler(PaTriangle2016CS20_Resize);
		this.MouseDown += new MouseEventHandler(PaTriangle2016CS20_MouseDown);
	}

	public void PaTriangle2016CS20_Paint(object sender, PaintEventArgs e){
		Graphics g = e.Graphics;

		for(int i=0; i<nCount; i++){
			if(i==3){
				pts_t = (PaTriangleDef)pts.Clone();
			}
			pen.Color = Color.FromArgb(pts.color.R*i/nCount,pts.color.G*i/nCount, pts.color.B*i/nCount);
			//pen.Color = Color.FromArgb(255*i/nCount, pts.color); // 使用 Alpha 通道CPU占用率很高
			g.DrawLines(pen, pts.points);
			g.DrawLine(pen, pts.points[0], pts.points[2]);
			PA_DoTriangleMoveItem();
		}
		pts = (PaTriangleDef)pts_t.Clone();
		PA_DoTriangleNextColor();
	}

	public void PaTriangle2016CS20_Resize(object sender, EventArgs e){
		PA_DoTriangleInit();
		this.Invalidate();
	}
	public void PaTriangle2016CS20_MouseDown(object sender, MouseEventArgs e){
		if (e.Button == System.Windows.Forms.MouseButtons.Right){
			PA_DoTriangleInit();
			this.Invalidate();
		}
	}

	public void PaTriangle2016CS20_Timer(object sender){
		this.Invalidate();
	}
	public void PA_DoTriangleInit(){
		nClientWidth = ClientRectangle.Width;
		nClientHeight = ClientRectangle.Height;
		for(int i=0; i<3; i++){
			pts.points[i].X = rand.Next(nClientWidth);
			pts.points[i].Y = rand.Next(nClientHeight);
			pts.dx[i] = rand.Next(2, 5);
			pts.dy[i] = rand.Next(2, 5);
			pts.color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
		}
		pts.dr = pts.dg = pts.db = 5;
	}

	public void PA_DoTriangleMoveItem(){
		int nx, ny;
		for(int i=0; i<3; i++){
			nx = pts.points[i].X + pts.dx[i];
			if(pts.dx[i]>0 && nx>nClientWidth || pts.dx[i]<0 && nx<0){
				pts.dx[i] = -pts.dx[i];
			}else{
				pts.points[i].X = nx;
			}
			ny = pts.points[i].Y + pts.dy[i];
			if(pts.dy[i]>0 && ny>nClientHeight || pts.dy[i]<0 && ny<0){
				pts.dy[i] = -pts.dy[i];
			}else{
				pts.points[i].Y = ny;
			}
		}
	}

	public void PA_DoTriangleNextColor(){
		int r = pts.color.R, g=pts.color.G, b=pts.color.B;
		int nr, ng, nb;
		nb = b + pts.db;
		if(pts.db>0 && nb>255 || pts.db<0 && nb<0){
			pts.db = -pts.db;
			ng = g + pts.dg;
			if(pts.dg>0 && ng>255 || pts.dg<0 && ng<0){
				pts.dg = -pts.dg;
				nr = r + pts.dr;
				if(pts.dr>0 && nr>255 || pts.dr<0 && nr<0){
					pts.dr = -pts.dr;
				}else{
					r = nr;
				}
			}else{
				g = ng;
			}
		}else{
			b = nb;
		}
		pts.color = Color.FromArgb(r, g, b);
		//pts.color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
	}

	public static void Main(string[] args){
		Application.Run(new PaTriangle2016CS20());
	}
}


class PaTriangleDef : ICloneable{
	public Point[] points = new Point[3];
	public int[] dx = new int[3];
	public int[] dy = new int[3];
	public Color color;
	public int dr, dg, db;
	public object Clone(){
		PaTriangleDef pt = new PaTriangleDef();
		for(int i=0; i<3; i++){
			pt.points[i] = this.points[i];
			pt.dx[i] = this.dx[i];
			pt.dy[i] = this.dy[i];
		}
		pt.color = this.color;
		pt.dr = this.dr;
		pt.dg = this.dg;
		pt.db = this.db;
		return pt;
	}
};

