//PaBalls2016CS20Alpha

using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using System.Drawing.Imaging;

public class PaBalls2016CS20Alpha : Form{
	private ResourceManager rm = new ResourceManager("PaBalls2016CS20Alpha",
		System.Reflection.Assembly.GetExecutingAssembly());
	private Image imgBgJpeg;
	private Image[] A_imgBalls = new Image[10];
	private ImageAttributes imgAttrib;
	private Random rand;
	private System.Threading.Timer timer;

	private BallDef[] A_objBalls = new BallDef[50];

	private int nCount = 50;

	private int nBgJpegWidth;
	private int nBgJpegHeight;
	private int nClientWidth;
	private int nClientHeight;
	private int nBallRadius;
	private int nBallDiameter;
	private int nRangeSpaceX;
	private int nRangeSpaceY;
	private int nMinD;
	private int nRangeD;
	private int nBallIconID=0;

	private bool isRunning = false;
	private bool isMinimized = false;

	private ToolBar toolBar1 = new ToolBar();
	private ImageList imagelist1 = new ImageList();

	private int nTimerInterval;
	private int nPathID=0;

	private double PI2;
	private int nCount1;
	private int nCount2;
	private double fRotate1;
	private double fRotate2;

	private int nStartY;

	protected override Size DefaultSize{
		get{
			return new Size(600, 450);
		}
	}

	public PaBalls2016CS20Alpha(){
		this.Text = "PaBalls2016CS20Alpha";
		this.BackColor = System.Drawing.Color.Black;
		this.StartPosition = FormStartPosition.CenterScreen;
		this.DoubleBuffered = true;
		this.Icon = (Icon)rm.GetObject("this.ico");

		PA_DoInitToolbar();

		//this.ClientSize = new Size(600, 400);
		this.MinimumSize = new Size(200, 180);
		this.Paint += new PaintEventHandler(PaBalls2016CS20Alpha_Paint);
		this.Resize += new EventHandler(PaBalls2016CS20Alpha_Resize);

		PA_DoAppInitialize();
		PA_DoBallsInit();

		timer = new System.Threading.Timer(PaBalls2016CS20Alpha_Timer, null, 0, nTimerInterval);
		isRunning = true;

	}

	protected void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e){
		int nIndex = toolBar1.Buttons.IndexOf(e.Button);
		switch (nIndex){
			case 0:	PA_DoBallsSwitchImage(); break;
			case 1:	PA_DoBallsSwitchImageX(); break;

			case 3: PA_DoEllipsePath(); break;
			case 4:	PA_DoBallsMovePlaced(); break;

			case 6: PA_DoSwitchTimer(); break;
		}
	}

	public void PA_DoSwitchTimer(){
		if(isRunning){
			timer.Change(System.Threading.Timeout.Infinite, nTimerInterval);
		}else{
			timer.Change(0, nTimerInterval);
		}
		isRunning = !isRunning;
	}

	public void PA_DoInitToolbar(){
		imagelist1.ImageSize = new Size(16, 15);
		imagelist1.Images.Add((Image)(rm.GetObject("button-01.png")));
		imagelist1.Images.Add((Image)(rm.GetObject("button-02.png")));
		imagelist1.Images.Add((Image)(rm.GetObject("button-03.png")));
		imagelist1.Images.Add((Image)(rm.GetObject("button-04.png")));
		imagelist1.Images.Add((Image)(rm.GetObject("button-05.png")));

		toolBar1.ImageList = imagelist1;
		toolBar1.ButtonSize = new System.Drawing.Size(16, 15);
		//toolBar1.Appearance = ToolBarAppearance.Flat;
		int nButtonsCount = 7;
		ToolBarButton[] A_toolBarButtons = new ToolBarButton[7];
		int n=0;
		for(int i=0; i<nButtonsCount; i++){
			A_toolBarButtons[i] = new ToolBarButton();
			if(i==2 || i==5){
				A_toolBarButtons[i].Style = ToolBarButtonStyle.Separator;
			}else{
				A_toolBarButtons[i].ImageIndex = n;
				n++;
			}
			toolBar1.Buttons.Add(A_toolBarButtons[i]);
		}
		Controls.Add(toolBar1);
		toolBar1.ButtonClick += new ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
		nStartY = toolBar1.Height;
	}

	public void PA_DoAppInitialize(){
		string[] A_strBalls = new string[10]{"ball-01.png", "ball-02.png", "ball-03.png", "ball-04.png", "ball-05.png",
			"ball-06.png", "ball-07.png", "ball-08.png", "ball-09.png", "ball-10.png"};

		int i;
		rand = new Random(unchecked((int)(DateTime.Now.Ticks)));
		nCount = 50;
		nTimerInterval = 20;
		nBallIconID = 7;
		nMinD = 2;
		nRangeD = 20;
		nBallRadius = 40;
		nBallDiameter = nBallRadius + nBallRadius;

		PI2 = Math.PI + Math.PI;
		nCount1 = nCount / 3;
		nCount2 = nCount - nCount1;
		fRotate1 = 0;
		fRotate2 = 0;
		nPathID = 0;

		//rm = new ResourceManager("PaBalls2016CS20Alpha",Assembly.GetExecutingAssembly());
		imgBgJpeg = ((Image)(rm.GetObject("bg.jpg")));
		nBgJpegWidth = imgBgJpeg.Width;
		nBgJpegHeight = imgBgJpeg.Height;

		for(i=0; i<10; i++){
			A_imgBalls[i] = ((Image)(rm.GetObject(A_strBalls[i])));
		}
		imgAttrib = new ImageAttributes(); ;
		imgAttrib.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);

		for(i=0; i<nCount; i++){
			A_objBalls[i] = new BallDef();
		}
	}

	private void PA_DoBallsMovePlaced(){
		nPathID = 0;
		for(int i=0; i<nCount; i++){
			A_objBalls[i].Init(nRangeSpaceX, nRangeSpaceY, nMinD, nRangeD, rand.Next(10), rand);
		}
		this.Invalidate();
	}

	private void PA_DoBallsMove(){
		for(int i=0; i<nCount; i++){
			A_objBalls[i].Move();
		}
		this.Invalidate();
	}

	private void PA_DoBallsSwitchImage(){
		nBallIconID = (nBallIconID+1) % 10;
		for(int i=0; i<nCount; i++){
			A_objBalls[i].SetIcon(nBallIconID);
		}
		this.Invalidate();
	}

	private void PA_DoBallsSwitchImageX(){
		for(int i=0; i<nCount; i++){
			A_objBalls[i].SetIcon(rand.Next(10));
		}
		this.Invalidate();
	}

	private void PA_DoEllipsePath(){
		nPathID = 1;
		PA_DoEllipsePathPlaced();
	}

	private void PA_DoEllipsePathPlaced(){
		int i;
		int n = 0;
		int a;
		int b;
		double fAngle;
		Point pt = new Point((nClientWidth - nBallDiameter)/2, (nClientHeight - nBallDiameter)/2);
		a = nClientWidth / 4;
		b = nClientHeight / 8;
		for(i=0; i<nCount1; i++){
			fAngle = PI2 * i / nCount1 + fRotate1;
			A_objBalls[n].x = pt.X + (int) (a * Math.Sin(fAngle));
			A_objBalls[n].y = pt.Y + (int) (b * Math.Cos(fAngle));
			A_objBalls[n].dx=0;
			A_objBalls[n].dy=0;
			n++;
		}

		a = nClientWidth * 3 / 7;
		b = nClientHeight * 2 / 7;
		for(i=0; i<nCount2; i++){
			fAngle = PI2 - PI2 * i / nCount2 - fRotate2;
			A_objBalls[n].x = pt.X + (int) (a * Math.Sin(fAngle));
			A_objBalls[n].y = pt.Y + (int) (b * Math.Cos(fAngle));
			A_objBalls[n].dx=0;
			A_objBalls[n].dy=0;
			n++;
		}
		this.Invalidate();
	}

	private void PA_DoEllipsePathMove(){
		fRotate1 += PI2 / 100;
		fRotate2 += PI2 / 200;
		if(fRotate1>PI2) fRotate1=0;
		if(fRotate2>PI2) fRotate2=0;
		PA_DoEllipsePathPlaced();
	}

	public void PaBalls2016CS20Alpha_Paint(object sender, PaintEventArgs e){
		Graphics g = e.Graphics;

		if(isMinimized) return;

		g.DrawImage(imgBgJpeg, new System.Drawing.Rectangle(
			(nClientWidth-nBgJpegWidth)/2,(nClientHeight-nBgJpegHeight)/2, nBgJpegWidth, nBgJpegHeight),
			0, 0, nBgJpegWidth, nBgJpegHeight, GraphicsUnit.Pixel,imgAttrib);

		for(int i=0; i<nCount; i++){
			//g.DrawImage(A_imgBalls[A_objBalls[i].nIconID], A_objBalls[i].x, A_objBalls[i].y + nStartY);
			g.DrawImage(A_imgBalls[A_objBalls[i].nIconID],
				new System.Drawing.Rectangle(A_objBalls[i].x, A_objBalls[i].y + nStartY, nBallDiameter, nBallDiameter),
				0, 0, nBallDiameter, nBallDiameter, GraphicsUnit.Pixel,imgAttrib);
		}

	}

	public void PA_DoBallsInit(){
		if(ClientRectangle.Width>0 && ClientRectangle.Height>0){
			nClientWidth = ClientRectangle.Width;
			nClientHeight = ClientRectangle.Height;
			nRangeSpaceX = nClientWidth - nBallDiameter - nStartY;
			nRangeSpaceY = nClientHeight - nBallDiameter - nStartY;
			if(nRangeSpaceX>0 && nRangeSpaceY>0){
				if(nPathID==0){
					PA_DoBallsMovePlaced();
				}else{
					PA_DoEllipsePathPlaced();
				}
				this.Invalidate();
				isMinimized = false;
			}else{
				isMinimized = true;
			}
		}else{
			isMinimized = true;
		}
	}

	public void PaBalls2016CS20Alpha_Resize(object sender, EventArgs e){
		PA_DoBallsInit();
	}

	public void PaBalls2016CS20Alpha_Timer(object sender){
		if(nPathID==0){
			PA_DoBallsMove();
		}else{
			PA_DoEllipsePathMove();
		}
	}

	public static void Main(string[] args){
		Application.Run(new PaBalls2016CS20Alpha());
	}
}

class BallDef {
	public int x, y;
	public int dx, dy;
	public int nIconID;
	public int nMaxX, nMaxY;
	public BallDef(){}
	public void SetIcon(int nIconID){ this.nIconID = nIconID; }
	public void Init(int nMaxX, int nMaxY, int nMinD, int nRangeD, int nIconID, Random rand){
		this.nMaxX = nMaxX;
		this.nMaxY = nMaxY;
		this.nIconID = nIconID;

		this.x = rand.Next(nMaxX);
		this.y = rand.Next(nMaxY);
		this.dx = nMinD+rand.Next(nRangeD);
		this.dy = nMinD+rand.Next(nRangeD);
	}
	public void Move(){
		int nx, ny;
		nx = x + dx;
		if(nx>nMaxX || nx<0){
			dx = -dx;
		}else{
			x = nx;
		}
		ny = y + dy;
		if(ny>nMaxY || ny<0){
			dy = -dy;
		}else{
			y= ny;
		}
	}
}