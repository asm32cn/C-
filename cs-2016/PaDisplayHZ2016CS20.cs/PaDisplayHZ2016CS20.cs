using System;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;

class PaDisplayHZ2016CS20 : Form {

	private ResourceManager rm = new ResourceManager("PaDisplayHZ2016CS20",
		System.Reflection.Assembly.GetExecutingAssembly());

	int nClientWidth, nClientHeight;
	byte[] A_matrix = null;
	//SolidBrush brush1 = new SolidBrush(Color.FromArgb(255, 255, 0));
	//SolidBrush brush2 = new SolidBrush(Color.FromArgb(31, 31, 31));
	byte[,] A_nMatrixBuffer = new byte[24,48];
	byte[,] A_nDisplayCache = new byte[24,48];
	byte[,] A_nDisplayBuffer = new byte[24,48];
	int nPoints = 24;
	int nPointsHF = 12;
	int nCountX = 48;
	byte[] A_mask = {128, 64, 32, 16, 8, 4, 2, 1};
	System.Threading.Timer timer1;
	int nStart = 0;
	int nStart1 = 0;
	int nActionID = 5;
	int nActionID1 = 0;
	int nActionCount = 20;
	Random rand = new Random();
	int nSleep = 0;
	int d, d1, nStartX, nStartY;
	bool isRefresh = true;

	protected override Size DefaultSize{
		get{
			return new Size(600, 450);
		}
	}

	public PaDisplayHZ2016CS20(){
		this.Text = "PaDisplayHZ2016CS20";
		this.BackColor = Color.Black;
		this.StartPosition = FormStartPosition.CenterScreen;
		this.DoubleBuffered = true;
		this.MinimumSize = new Size(550, 350);
		this.Icon = (Icon)(rm.GetObject("this.ico"));

		A_matrix = (byte[])(rm.GetObject("matrix24f.bin"));

		int id=0;
		for(int n=0; n<2; n++){
			int stx = n * nPoints;
			for(int y=0; y<nPoints; y++){
				for(int x=0; x<nPoints; x++){
					int pos = x % 8;
					A_nMatrixBuffer[y, stx + x] = (byte)((A_matrix[id] & A_mask[pos])>0 ? 1 : 0);
					if(pos==7){
						id++;
					}
				}
			}
		}

		PA_DoFormResize();

		this.Paint += new PaintEventHandler(PaDisplayHZ2016CS20_Paint);
		this.Resize += new EventHandler(PaDisplayHZ2016CS20_Resize);

		timer1 = new System.Threading.Timer(PaDisplayHZ2016CS20_Timer, null, 0, 20);
	}

	private void PaDisplayHZ2016CS20_Timer(object sender){
		PA_DoAction();
		PA_DoDisplay();
		//this.Invalidate();
	}

	private void PaDisplayHZ2016CS20_Resize(object sender, EventArgs e){
		if(ClientRectangle.Width>0 && ClientRectangle.Height>0){
			PA_DoFormResize();
		}
	}

	private void PA_DoFormResize(){
		nClientWidth = ClientRectangle.Width;
		nClientHeight = ClientRectangle.Height;
		d1 = nClientWidth / 48;
		d = d1 - 1;
		nStartX = (nClientWidth - d1 * 48 /*nDisplayW*/) / 2;
		nStartY = (nClientHeight - d1 * 24 /*nDisplayH*/) / 2;
		//isRefresh = true;
		this.Invalidate();
	}

	private void PaDisplayHZ2016CS20_Paint(object sender, PaintEventArgs e){
		//Graphics g = e.Graphics;
		isRefresh = true;
	}

	private void PA_DoDisplay(){
		using(Graphics g = this.CreateGraphics()){
			SolidBrush brush1 = new SolidBrush(Color.FromArgb(255, 255, 0));
			SolidBrush brush2 = new SolidBrush(Color.FromArgb(31, 31, 31));

			for(int y=0; y<nPoints; y++){
				for(int x=0; x<nCountX; x++){
					if(isRefresh || A_nDisplayBuffer[y, x]!=A_nDisplayCache[y, x]){
						g.FillEllipse((A_nDisplayBuffer[y, x]==1 ? brush1 : brush2),
							nStartX + x*d1, nStartY + y*d1, d, d);
					}
					A_nDisplayCache[y, x] = A_nDisplayBuffer[y, x];
				}
			}
			isRefresh = false;
		}
	}

	private void PA_DoGetNextAction(){
		do{
			nActionID = rand.Next(nActionCount);
		}while(nActionID1==nActionID);
		nActionID1 = nActionID;
	}

	private void PA_DoAction(){
		if(nSleep>0){
			nSleep--;
		}else{
			switch(nActionID){
				case 1: PA_DoAction1(); break;
				case 2: PA_DoAction2(); break;
				case 3: PA_DoAction3(); break;
				case 4: PA_DoAction4(); break;
				case 5: PA_DoAction5(); break;
				case 6: PA_DoAction6(); break;
				case 7: PA_DoAction7(); break;
				case 8: PA_DoAction8(); break;
				case 9: PA_DoAction9(); break;
				case 10: PA_DoAction10(); break;
				case 11: PA_DoAction11(); break;
				case 12: PA_DoAction12(); break;
				case 13: PA_DoAction13(); break;
				case 14: PA_DoAction14(); break;
				case 15: PA_DoAction15(); break;
				case 16: PA_DoAction16(); break;
				case 17: PA_DoAction17(); break;
				case 18: PA_DoAction18(); break;
				case 19: PA_DoAction19(); break;
				default: PA_DoAction0(); break;
			}
			if(nStart1==0){
				nSleep = 25;
				PA_DoGetNextAction();
			}
		}
	}

	private void PA_DoAction0(){
		nStart = nCountX - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(x<nStart){
					A_nDisplayBuffer[y, x] = 0;
				}else{
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, x-nStart];
				}
			}
		}
		nStart1 = (nStart1 + 1) % nCountX;
	}

	private void PA_DoAction1(){
		nStart = nCountX - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(x<nStart1){
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, (nStart + x - 1)];
				}else{
					A_nDisplayBuffer[y, x] = 0;
				}
			}
		}
		nStart1 = (nStart1 + 1) % nCountX;
	}

	private void PA_DoAction2(){
		nStart = nCountX - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, (x + nStart1) % nCountX];
			}
		}
		nStart1 = (nStart1 + 1) % nCountX;
	}

	private void PA_DoAction3(){
		nStart = nCountX - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, (nCountX + x + nStart - 1) % nCountX];
			}
		}
		nStart1 = (nStart1 + 1) % nCountX;
	}

	private void PA_DoAction4(){
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(x < nPoints - nStart1 || x > nPoints + nStart1){
					A_nDisplayBuffer[y, x] = 0;
				}else{
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, x];
				}
			}
		}
		nStart1 = (nStart1 + 1) % nPoints;
	}

	private void PA_DoAction5(){
		nStart = nPoints - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(x < nPoints-nStart + 1 || x > nPoints + nStart - 1){
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, x];
				}else{
					A_nDisplayBuffer[y, x] = 0;
				}
			}
		}
		nStart1 = (nStart1 + 1) % nPoints;
	}

	private void PA_DoAction6(){
		nStart = nCountX - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(x<nStart1){
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, x];
				}else{
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, nStart1];
				}
			}
		}
		nStart1 = (nStart1 + 1) % nCountX;
	}

	private void PA_DoAction7(){
		nStart = nCountX - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(x<nStart){
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, nStart - 1];
				}else{
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, x];
				}
			}
		}
		nStart1 = (nStart1 + 1) % nCountX;
	}

	private void PA_DoAction8(){
		nStart = nCountX - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(x<nStart1){
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, x];
				}else{
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, nStart1 + (int)((x - nStart1) / 2) ];
				}
			}
		}
		nStart1 = (nStart1 + 1) % nCountX;
	}

	private void PA_DoAction9(){
		nStart = nCountX - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(x<nStart){
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, nStart1 + (int)((nStart-x) / 2)];
				}else{
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, x];
				}
			}
		}
		nStart1 = (nStart1 + 1) % nCountX;
	}

	private void PA_DoAction10(){
		nStart = nPoints - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(y<nStart1){
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, x];
				}else{
					A_nDisplayBuffer[y, x] = 0;
				}
			}
		}
		nStart1 = (nStart1 + 1) % nPoints;
	}

	private void PA_DoAction11(){
		nStart = nPoints - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(y<nStart){
					A_nDisplayBuffer[y, x] = 0;
				}else{
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, x];
				}
			}
		}
		nStart1 = (nStart1 + 1) % nPoints;
	}

	private void PA_DoAction12(){
		nStart = nPoints - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(y<nStart1){
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y  + nStart, x];
				}else{
					A_nDisplayBuffer[y, x] = 0;
				}
			}
		}
		nStart1 = (nStart1 + 1) % nPoints;
	}

	private void PA_DoAction13(){
		nStart = nPoints - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(y<nStart){
					A_nDisplayBuffer[y, x] = 0;
				}else{
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y - nStart, x];
				}
			}
		}
		nStart1 = (nStart1 + 1) % nPoints;
	}

	private void PA_DoAction14(){
		nStart = nPointsHF - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(y<nStart || y>nPointsHF + nStart1){
					A_nDisplayBuffer[y, x] = 0;
				}else{
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, x];
				}
			}
		}
		nStart1 = (nStart1 + 1) % nPointsHF;
	}

	private void PA_DoAction15(){
		nStart = nPointsHF - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(y<nPointsHF - nStart + 1 || y>nPointsHF + nStart - 2){
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, x];
				}else{
					A_nDisplayBuffer[y, x] = 0;
				}
			}
		}
		nStart1 = (nStart1 + 1) % nPointsHF;
	}

	private void PA_DoAction16(){
		nStart = nPoints - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(y<nStart1){
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, x];
				}else{
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[nStart1 + (int)((y - nStart1) / 2), x];
				}
			}
		}
		nStart1 = (nStart1 + 1) % nPoints;
	}

	private void PA_DoAction17(){
		nStart = nPoints - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(y<nStart){
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[nStart1 + (int)((nStart - y) / 2), x];
				}else{
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, x];
				}
			}
		}
		nStart1 = (nStart1 + 1) % nPoints;
	}

	private void PA_DoAction18(){
		nStart = nPoints - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(y<nStart1){
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, x];
				}else{
					int y1 = nStart1 + (int)((y - nStart1) / 2);
					A_nDisplayBuffer[y, x] = (((y1 % 2)==0) ^ (x % 2==0) ? A_nMatrixBuffer[y1, x] : (byte)0);
				}
			}
		}
		nStart1 = (nStart1 + 1) % nPoints;
	}

	private void PA_DoAction19(){
		nStart = nPoints - nStart1;
		for(int y = 0; y<nPoints; y++){
			for(int x = 0; x<nCountX; x++){
				if(y<nStart){
					int y1 = nStart1 + (int)((nStart - y) / 2);
					A_nDisplayBuffer[y, x] = (((y1 % 2)==0) ^ (x % 2==0) ? A_nMatrixBuffer[y1 , x] : (byte)0);
				}else{
					A_nDisplayBuffer[y, x] = A_nMatrixBuffer[y, x];
				}
			}
		}
		nStart1 = (nStart1 + 1) % nPoints;
	}

	public static void Main(string[] args){
		Application.Run(new PaDisplayHZ2016CS20());
	}
}