using System;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;
using System.Runtime.InteropServices; // [DllImport("dll file")]

public class PaTransparentForm2016CS20 :Form {
	private ResourceManager rm = new ResourceManager("PaTransparentForm2016CS20",
		System.Reflection.Assembly.GetExecutingAssembly());

	//需添加using System.Runtime.InteropServices;
	[DllImport("user32.dll")]
	public static extern bool ReleaseCapture();
	[DllImport("user32.dll")]
	public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

	public PaTransparentForm2016CS20(){
		//BackgroundImage = Image.FromFile(@"res\heart.png");
		BackgroundImage = (Image)(rm.GetObject("heart.png"));
		FormBorderStyle = FormBorderStyle.None;
		StartPosition = FormStartPosition.CenterScreen;
		BackgroundImageLayout = ImageLayout.None;
		//Color c = Color.Blue;
		Color c = Color.White;
		BackColor = c;
		TransparencyKey = c;
		//Width = BackgroundImage.Width;
		//Height = BackgroundImage.Height;
		this.ClientSize = new Size(BackgroundImage.Width, BackgroundImage.Height);
		this.MouseDown += new MouseEventHandler(PaTransparentForm2016CS20_MouseDown);
	}

	public void PaTransparentForm2016CS20_MouseDown(object sender, MouseEventArgs e){
		if(e.Button == MouseButtons.Left){
			ReleaseCapture(); //释放鼠标捕捉
			//发送左键点击的消息至该窗体(标题栏)
			SendMessage(this.Handle, 0xa1, 0x02, 0);
		}
	}

	public static void Main(string[] args){
		Application.Run(new PaTransparentForm2016CS20());
	}
}