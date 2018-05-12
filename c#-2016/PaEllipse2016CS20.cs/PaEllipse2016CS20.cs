using System;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;

public class PaEllipse2016CS20 : Form{
	private ResourceManager rm = new ResourceManager("PaEllipse2016CS20",
		System.Reflection.Assembly.GetExecutingAssembly());
	private System.Threading.Timer timer;
	private double PI2 = Math.PI + Math.PI;
	private double fStartAngle = 0;
	private PaEllipseDef pes1 = new PaEllipseDef(0, 0, 300, 75, 0, 0);
	private PaEllipseDef pes2 = new PaEllipseDef(0, 0, 50, 200, 0, 0);
	private SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 0));

	protected override Size DefaultSize{
		get{
			return new Size(600, 450);
		}
	}

	public PaEllipse2016CS20(){
		this.Text = "PaEllipse2016CS20";
		//this.ClientSize = new Size(600, 450);
		this.BackColor = System.Drawing.Color.Black;
		this.StartPosition = FormStartPosition.CenterScreen;
		this.DoubleBuffered = true;
		this.Icon = (Icon)rm.GetObject("this.ico");

		this.Paint += new PaintEventHandler(PaEllipse2016CS20_Paint);
		this.Resize += new EventHandler(PaEllipse2016CS20_Resize);

		timer = new System.Threading.Timer(PaEllipse2016CS20_Timer, null, 0, 20);
	}

	public void PaEllipse2016CS20_Paint(object sender, PaintEventArgs e){
		Graphics g = e.Graphics;

		int x0 = ClientRectangle.Width / 2;
		int y0 = ClientRectangle.Height / 2;
		double step = PI2 / 40;
		int r = 0;
		int d = 0;
		Point point = new Point();
		for(double i=0; i<PI2; i+=step){
			double sin1 = Math.Sin(i + fStartAngle);
			double cos1 = Math.Cos(i + fStartAngle);
			r = r==3 ? 6 : 3;
			d = r + r;
			//int x1, y1;
			point.X = (int)(pes1.a * sin1);
			point.Y = (int)(pes1.b * cos1);
			point = Rotate(point, fStartAngle);
			point.X += x0;
			point.Y += y0;
			g.FillEllipse(brush, point.X - r, point.Y - r, d, d);

			point.X = (int)(pes2.a * sin1);
			point.Y = (int)(pes2.b * cos1);
			point = Rotate(point, fStartAngle);
			point.X += x0;
			point.Y += y0;
			g.FillEllipse(brush, point.X - r, point.Y - r, d, d);
		}
	}

	public void PaEllipse2016CS20_Resize(object sender, EventArgs e){
	}

	public void PaEllipse2016CS20_Timer(object sender){
		this.Invalidate();
		PA_DoEllipseRotate();
	}

	public void PA_DoEllipseRotate(){
		fStartAngle += PI2 / 160;
		if(fStartAngle>=PI2) fStartAngle=0;
	}

	private Point Rotate(Point point, double angle){
		double sin1 = Math.Sin(angle);
		double cos1 = Math.Cos(angle);
		int x1 = point.X, y1 = point.Y;
		point.X = (int)(cos1 * x1 - sin1 * y1);
		point.Y = (int)(cos1 * y1 + sin1 * x1);
		return point;
	}

	public static void Main(string[] args){
		Application.Run(new PaEllipse2016CS20());
	}
}

class PaEllipseDef{
	public int x, y;
	public int a, b;
	public double angle;
	public double rotate;
	public PaEllipseDef(){}
	public PaEllipseDef(int x, int y, int a, int b, double angle, double rotate){
		this.x = x;
		this.y = y;
		this.a = a;
		this.b = b;
		this.angle = angle;
		this.rotate = rotate;
	}
}