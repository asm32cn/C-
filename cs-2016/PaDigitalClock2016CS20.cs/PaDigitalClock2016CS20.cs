using System;
using System.Drawing;
using System.Resources;  // ResourceManager
using System.Reflection; // Assembly
using System.Windows.Forms; 

public class PaDigitalClock2016CS20 : Form {
	private ResourceManager rm = new ResourceManager("PaDigitalClock2016CS20",
		Assembly.GetExecutingAssembly());
	private ushort[] charMask = new ushort[12]{2048, 1024, 512, 256, 128, 64, 32, 16, 8, 4, 2, 1};
	private byte[] byteDigitMatrix;
	private Bitmap bmpDigits = null;
	private SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 0));
	private int[] A_nDigits = new int[11];
	private int[] A_nDigits1 = new int[11];
	private bool isVisible = false;
	private bool isRefresh = true;
	private int d;
	private int nClientWidth;
	private int nClientHeight;
	private int nItemW;
	private int nItemH;
	private int nStartX;
	private int nStartY;
	private int nSecond1 = 0; // 修正补丁，每秒强制重绘所有字符一次
	private System.Threading.Timer timer;

	protected override Size DefaultSize{
		get{
			return new Size(750, 450);
		}
	}

	public PaDigitalClock2016CS20(){
		this.Text = "PaDigitalClock2016CS20";
		this.BackColor = Color.Black;
		//this.ClientSize = new Size(750, 450);
		this.StartPosition = FormStartPosition.CenterScreen;
		this.MinimumSize = new Size(300, 180);

		this.DoubleBuffered = true;

		this.Paint += new PaintEventHandler(PaDigitalClock2016CS20_Paint);
		this.Resize += new EventHandler(PaDigitalClock2016CS20_Resize);

		PA_DoAppInitialize();

		timer = new System.Threading.Timer(PaDigitalClock2016CS20_Timer, null, 0, 10);
	}

	public void PA_DoAppInitialize(){
		this.Icon = (Icon)(rm.GetObject("this.ico"));
		byteDigitMatrix = (byte[])(rm.GetObject("matrix.bin"));

		for(int i=0; i<11; i++){
			A_nDigits1[i] =0;
		}

		PA_DoFormResize();

	}

	public void PA_DoFormResize(){
		nClientWidth = ClientRectangle.Width;
		nClientHeight = ClientRectangle.Height;
		isRefresh = true;
		if(nClientWidth>0 && nClientHeight>0){
			isVisible = true;

			d = nClientWidth / 12 / 11; // 显示 12:00:00.00格式 (11字符,每个字符12点宽)
			if(d<2) d=2;
			nItemW = d * 12;
			nItemH = d * 22;
			nStartX = (nClientWidth - nItemW * 11)/2;
			nStartY = (nClientHeight - nItemH)/2;
			int nBitmapDigitW = nItemW * 13;

			if(bmpDigits!=null) bmpDigits.Dispose();
			bmpDigits = new Bitmap(nBitmapDigitW, nItemH);
			using(Graphics g1 = Graphics.FromImage(bmpDigits)){
				brush.Color = Color.FromArgb(0, 0, 255);
				g1.FillRectangle(brush, 0, 0, nBitmapDigitW, nItemH);
				brush.Color = Color.FromArgb(255, 255, 255);
				int nOffset=0;
				//int d = 9;
				for(int n=0; n<12; n++){
					int x = n * nItemW;
					for(int i=0; i<22; i++){
						int ch = byteDigitMatrix[nOffset] | (byteDigitMatrix[nOffset+1]<<8);
						int y = i * d;
						for(int ii=0; ii<12; ii++){
							if((ch & charMask[ii])>0){
								g1.FillEllipse(brush, x + ii * d, y, d, d);
							}
						}
						nOffset += 2;
					}
				}
			}
			this.Invalidate();
		}else{
			isVisible = false;
		}
	}

	public void PA_DoDisplay(){
		if(isVisible==false) return;

		using(Graphics g = this.CreateGraphics()){

			DateTime dtNow = DateTime.Now;
			int nSplitter1 = dtNow.Millisecond>500 ? 12 : 10;

			if(nSecond1!=dtNow.Second){
				nSecond1 = dtNow.Second;
				isRefresh = true; // 每秒钟强制重绘所有字符
			}

			A_nDigits[0] = dtNow.Hour / 10;
			A_nDigits[1] = dtNow.Hour % 10;
			A_nDigits[2] = nSplitter1;
			A_nDigits[3] = dtNow.Minute / 10;
			A_nDigits[4] = dtNow.Minute % 10;
			A_nDigits[5] = nSplitter1;
			A_nDigits[6] = dtNow.Second / 10;
			A_nDigits[7] = dtNow.Second % 10;
			A_nDigits[8] = 11;
			A_nDigits[9] = dtNow.Millisecond / 100 % 10;
			A_nDigits[10] = dtNow.Millisecond / 10 % 10;

			Rectangle rectDest = new Rectangle(0, nStartY, nItemW, nItemH);
			try{
				for(int i=0; i<11; i++){
					if(isRefresh || A_nDigits[i]!=A_nDigits1[i]){
						rectDest.X = nStartX + i * nItemW;
						g.DrawImage(bmpDigits, rectDest, 
							A_nDigits[i]*nItemW, 0, nItemW, nItemH, GraphicsUnit.Pixel);
					}
				}
				Array.Copy(A_nDigits, A_nDigits1, A_nDigits.Length);
				isRefresh = false;
			}catch(Exception ex){
				Console.Write("Exception: " + ex.Message + "\r\n");
				isRefresh = true;
			}
		}
	}

	public void PaDigitalClock2016CS20_Paint(object sender, PaintEventArgs e){
		//Graphics g = e.Graphics;
		isRefresh = true;
		//PA_DoDisplay();
	}

	public void PaDigitalClock2016CS20_Resize(object sender, EventArgs e){
		PA_DoFormResize();
	}

	public void PaDigitalClock2016CS20_Timer(object sender){
		PA_DoDisplay();
		//this.Invalidate();
	}

	public static void Main(string[] args){
		Application.Run(new PaDigitalClock2016CS20());
	}
}