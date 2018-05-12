using System;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;

public class PaQix2016CS20 : Form{
	private ResourceManager rm = new ResourceManager("PaQix2016CS20",
		System.Reflection.Assembly.GetExecutingAssembly());
	private Random rand;
	private int nClientWidth;
	private int nClientHeight;
	private System.Threading.Timer timer;
	private Pen pen;
	private PaQixDef pqs = new PaQixDef();
	private PaQixDef pqs_t;
	private int nCount = 200;
	private bool isColorEx = true;

	protected override Size DefaultSize{
		get{
			return new Size(600, 450);
		}
	}

	public PaQix2016CS20(){
		this.Text = "PaQix2016CS20";
		//this.ClientSize = new Size(600, 450);
		this.BackColor = System.Drawing.Color.Black;
		this.StartPosition = FormStartPosition.CenterScreen;
		this.DoubleBuffered = true;
		this.Icon = (Icon)rm.GetObject("this.ico");

		rand = new Random(unchecked((int)DateTime.Now.Ticks));
		pen = new Pen(System.Drawing.Color.White, 1);

		this.Paint += new PaintEventHandler(PaQix2016CS20_Paint);
		this.Resize += new EventHandler(PaQix2016CS20_Resize);
		this.MouseDown += new MouseEventHandler(PaQix2016CS20_MouseDown);

		PA_DoQixInit();

		timer = new System.Threading.Timer(PaQix2016CS20_Timer, null, 0, 20);

	}

	public void PaQix2016CS20_Paint(object sender, PaintEventArgs e){
		Graphics g = e.Graphics;

		for(int i=0; i<nCount; i++){
			pen.Color = Color.FromArgb(pqs.color.R*i/nCount, pqs.color.G*i/nCount, pqs.color.B*i/nCount);
			if(i==10){
				pqs_t = (PaQixDef)pqs.Clone();
			}
			g.DrawLine(pen, pqs.points[0], pqs.points[1]);
			PA_DoQixMove();
			if(isColorEx){
				PA_DoQixColorNext2();
			}else{
				PA_DoQixColorNext1();
			}
		}
		pqs = (PaQixDef)pqs_t.Clone();
	}

	public void PaQix2016CS20_Resize(object sender, EventArgs e){
		PA_DoQixInit();
		this.Invalidate();
	}

	public void PaQix2016CS20_MouseDown(object sender, MouseEventArgs e){
		if(e.Button == System.Windows.Forms.MouseButtons.Left){
			isColorEx = !isColorEx;
		}else if(e.Button == System.Windows.Forms.MouseButtons.Right){
			PA_DoQixInit();
			this.Invalidate();
		}
	}

	public void PaQix2016CS20_Timer(object sender){
		this.Invalidate();
	}

	public void PA_DoQixInit(){
		nClientWidth = ClientRectangle.Width;
		nClientHeight = ClientRectangle.Height;
		for(int i=0; i<2; i++){
			pqs.points[i].X = rand.Next(nClientWidth);
			pqs.points[i].Y = rand.Next(nClientHeight);
			pqs.dx[i] = rand.Next(2, 5);
			pqs.dy[i] = rand.Next(2, 5);
		}
		pqs.color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
		pqs.dr = pqs.dg = pqs.db = 5;
	}

	public void PA_DoQixMove(){
		int nx, ny;
		for(int i=0; i<2; i++){
			nx = pqs.points[i].X + pqs.dx[i];
			if(pqs.dx[i]>0 && nx>nClientWidth || pqs.dx[i]<0 && nx<0){
				pqs.dx[i] = -pqs.dx[i];
			}else{
				pqs.points[i].X = nx;
			}
			ny = pqs.points[i].Y + pqs.dy[i];
			if(pqs.dy[i]>0 && ny>nClientHeight || pqs.dy[i]<0 && ny<0){
				pqs.dy[i] = -pqs.dy[i];
			}else{
				pqs.points[i].Y = ny;
			}
		}
	}

	public void PA_DoQixColorNext1(){
		int r = pqs.color.R, g=pqs.color.G, b=pqs.color.B;
		int nr, ng, nb;
		nb = b + pqs.db;
		if(pqs.db>0 && nb>255 || pqs.db<0 && nb<0){
			pqs.db = -pqs.db;
			ng = g + pqs.dg;
			if(pqs.dg>0 && ng>255 || pqs.dg<0 && ng<0){
				pqs.dg = -pqs.dg;
				nr = r + pqs.dr;
				if(pqs.dr>0 && nr>255 || pqs.dr<0 && nr<0){
					pqs.dr = -pqs.dr;
				}else{
					r = nr;
				}
			}else{
				g = ng;
			}
		}else{
			b = nb;
		}
		pqs.color = Color.FromArgb(r, g, b);
	}

	public void PA_DoQixColorNext2(){ // ²ÊºçÑÕÉ«
		int r = pqs.color.R, g=pqs.color.G, b=pqs.color.B;
		int nr, ng, nb;
		nb = b + pqs.db;
		if(pqs.db>0 && nb>255 || pqs.db<0 && nb<0){
			pqs.db = -pqs.db;
		}else{
			b = nb;
			ng = g + pqs.dg;
			if(pqs.dg>0 && ng>255 || pqs.dg<0 && ng<0){
				pqs.dg = -pqs.dg;
			}else{
				g = ng;
				nr = r + pqs.dr;
				if(pqs.dr>0 && nr>255 || pqs.dr<0 && nr<0){
					pqs.dr = -pqs.dr;
				}else{
					r = nr;
				}
			}
		}
		pqs.color = Color.FromArgb(r, g, b);
	}

	public static void Main(string[] args){
		Application.Run(new PaQix2016CS20());
	}
}

class PaQixDef : ICloneable {
	public Point[] points = new Point[2];
	public int[] dx = new int[2];
	public int[] dy = new int[2];
	public Color color;
	public int dr, dg, db;

	public object Clone(){
		PaQixDef pq = new PaQixDef();
		for(int i=0; i<2; i++){
			pq.points[i] = this.points[i];
			pq.dx[i] = this.dx[i];
			pq.dy[i] = this.dy[i];
		}
		pq.color = this.color;
		pq.dr = this.dr;
		pq.dg = this.dg;
		pq.db = this.db;
		return pq;
	}
}