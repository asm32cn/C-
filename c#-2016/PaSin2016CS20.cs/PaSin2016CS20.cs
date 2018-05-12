using System;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;

public class PaSin2016CS20 : Form{
	private ResourceManager rm = new ResourceManager("PaSin2016CS20",
		System.Reflection.Assembly.GetExecutingAssembly());
	private int nClientWidth;
	private int nClientHeight;
	private System.Threading.Timer timer;
	private double PI2 = Math.PI + Math.PI;
	private int nCount = 200;
	private int nOffset = 0;
	private int nStartY;
	private int nSizeY;
	private int nWidth1;
	private SolidBrush brush = new SolidBrush(Color.FromArgb(0, 255,0));

	protected override Size DefaultSize {
		get{
			return new Size(600, 450);
		}
	}

	public PaSin2016CS20(){
		this.Text = "PaSin2016CS20";
		//this.ClientSize = new Size(600, 450);
		this.BackColor = System.Drawing.Color.Black;
		this.StartPosition = FormStartPosition.CenterScreen;
		this.DoubleBuffered = true;
		this.Icon = (Icon)rm.GetObject("this.ico");

		PA_DoSin2016_Init();

		this.Paint += new PaintEventHandler(PaSin2016CS20_Paint);
		this.Resize += new EventHandler(PaSin2016CS20_Resize);

		timer = new System.Threading.Timer(PaSin2016CS20_Timer, null, 0, 10);
	}

	public void PaSin2016CS20_Paint(object sender, PaintEventArgs e){
		Graphics g = e.Graphics;

		for(int i=0; i<nCount; i++){
			int nStartX = nClientWidth * i / nCount;
			double sin1 = Math.Sin(PI2 * (i + nOffset) / nCount);
			int nHeight1 = (int)(sin1 * nSizeY);
			int nOffsetY = nHeight1<0 ? nHeight1 : (int)(nHeight1 * 0.9);
			int nHeight2 = nHeight1<0 ? -nHeight1 : nHeight1;
			g.FillRectangle(brush, nStartX, nStartY + nOffsetY, nWidth1, (int)(nHeight2 * 0.1));
		}
	}

	public void PaSin2016CS20_Resize(object sender, EventArgs e){
		PA_DoSin2016_Init();
		this.Invalidate();
	}

	public void PaSin2016CS20_Timer(object sender){
		nOffset = (nOffset + 5) % nCount;
		this.Invalidate();
	}

	public void PA_DoSin2016_Init(){
		nClientWidth = ClientRectangle.Width;
		nClientHeight = ClientRectangle.Height;
		nStartY = nClientHeight / 2;
		nWidth1 = nClientWidth / nCount / 2;
		nSizeY = nClientHeight / 2;
	}

	public static void Main(string[ ]args){
		Application.Run(new PaSin2016CS20());
	}
}