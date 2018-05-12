using System;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;  // ResourceManager

public class PaClock2016CS20 : Form{
	private ResourceManager rm = new ResourceManager("PaClock2016CS20",
		System.Reflection.Assembly.GetExecutingAssembly());
	private Bitmap bmpInterface = null;
	private Image imgInterface = null;
	private int nClientWidth;
	private int nClientHeight;
	private int nClockDiameter;
	private int nClockRadius;
	private double PI2 = Math.PI + Math.PI;
	private Pen pen = new Pen(Color.Black, 3);
	private Font font = new Font("Arial Black", 24);
	private SolidBrush brush = new SolidBrush(Color.FromArgb(87, 183, 119));
	private System.Threading.Timer timer;

	protected override Size DefaultSize{
		get{
			return new Size(600, 450);
		}
	}

	public PaClock2016CS20(){
		this.Text = "PaClock2016CS20";
		this.BackColor = Color.White;
		//this.ClientSize = new Size(600, 450);
		this.MinimumSize = new Size(450, 450);
		this.StartPosition = FormStartPosition.CenterScreen;
		this.DoubleBuffered = true;

		this.Paint += new PaintEventHandler(PaClock2016CS20_Paint);
		this.Resize += new EventHandler(PaClock2016CS20_Resize);

		PA_DoAppInitialize();
		PA_DoFormResize();
		DateTime dtNow = DateTime.Now;
		int nStep = 1000 - dtNow.Millisecond;
		timer = new System.Threading.Timer(PaClock2016CS20_Timer, null, nStep, 500);
	}

	public void PA_DoAppInitialize(){
		this.Icon = (Icon)(rm.GetObject("this.ico"));
		imgInterface = (Image)(rm.GetObject("interface.gif"));
	}

	public void PA_DoFormResize(){
		nClientWidth  = ClientRectangle.Width;
		nClientHeight = ClientRectangle.Height;
		if(nClientWidth>0 && nClientHeight>0){
			nClockRadius = (nClientWidth>nClientHeight ? nClientHeight : nClientWidth) *9 / 10 / 2;
			nClockDiameter = nClockRadius + nClockRadius;
			if(bmpInterface!=null) bmpInterface.Dispose();
			bmpInterface = new Bitmap(nClockDiameter, nClockDiameter);
			using(Graphics g1 = Graphics.FromImage(bmpInterface)){
				int x = (nClockDiameter - imgInterface.Width) / 2 + 15;
				int y = (nClockDiameter - imgInterface.Height) / 2;
				g1.DrawImage(imgInterface, x, y);
				pen.Color = Color.Black;
				pen.Width = 7;
				g1.DrawEllipse(pen, 3, 3, nClockDiameter-7, nClockDiameter-7);
				g1.DrawEllipse(pen, nClockRadius-5, nClockRadius-5, 10, 10);
				//PI / 
				for(int i=0; i<60; i++){
					double angle1 = PI2 * i / 60;
					double dx1 = Math.Sin(angle1) * nClockRadius;
					double dy1 = Math.Cos(angle1) * nClockRadius;
					double s1;
					if(i%5==0){
						pen.Width = 5;
						s1 = 0.9;
					}else{
						pen.Width = 3;
						s1 = 0.94;
					}
					g1.DrawLine(pen, nClockRadius + (int)dx1, nClockRadius - (int)dy1,
						nClockRadius + (int)(dx1 * s1), nClockRadius - (int)(dy1 * s1));
					string str = "" + (i/5);
					if(i%5==0){
						g1.DrawString(str, font, brush,
							(float)(nClockRadius + dx1 * 0.8 - 15),
							(float)(nClockRadius - dy1 * 0.8) -25);
					}
				}
			}
		}
	}

	public void PaClock2016CS20_Paint(object sender, PaintEventArgs e){
		Graphics g = e.Graphics;
		DateTime dtNow = DateTime.Now;
		int x = (nClientWidth - nClockDiameter)/2;
		int y = (nClientHeight - nClockDiameter)/2;
		g.DrawImage(bmpInterface, x, y);
		
		int cx = nClientWidth / 2;
		int cy = nClientHeight / 2;

		double fStart = 0.05;
		int[] penWidth = new int[3]{3, 5, 7};
		double[] A_fEnd = new double[3]{0.7, 0.5, 0.4};
		double[] A_fAngle = new double[3]{
			PI2 * dtNow.Second / 60,
				PI2 * (60 * dtNow.Minute + dtNow.Second) / 3600,
				PI2 * (60 * ( 60 * ( dtNow.Hour % 12 ) + dtNow.Minute) + dtNow.Second) / 43200};
		for(int i=0; i<3; i++){
			double dx1 = Math.Sin(A_fAngle[i]) * nClockRadius;
			double dy1 = Math.Cos(A_fAngle[i]) * nClockRadius;
			pen.Width = penWidth[i];
			g.DrawLine(pen,
				(float)(cx + dx1 * fStart), (float)(cy - dy1 * fStart),
				(float)(cx + dx1 * A_fEnd[i]), (float)(cy - dy1 * A_fEnd[i]));
		}
	}

	public void PaClock2016CS20_Resize(object sender, EventArgs e){
		PA_DoFormResize();
		this.Invalidate();
	}

	public void PaClock2016CS20_Timer(object sender){
		this.Invalidate();
	}

	public static void Main(string[] args){
		Application.Run(new PaClock2016CS20());
	}
}