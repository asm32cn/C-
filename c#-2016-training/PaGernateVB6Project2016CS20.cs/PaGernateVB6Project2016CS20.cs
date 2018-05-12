//PaGernateVB6Project2016CS20
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;

class PaGernateVB6Project2016CS20 : Form{
	private ResourceManager rm = new ResourceManager("PaGernateVB6Project2016CS20",
		System.Reflection.Assembly.GetExecutingAssembly());
	private TextBox textbox1 = new TextBox();
	private TextBox textbox2 = new TextBox();
	private Button button1 = new Button();
	private string m_strBaseFolder = @"E:\CODE\VB\VB6-2016\";
	private string m_strProjectName__conf = "PaSin2016VB6";
	//private string m_strCrlf = "\r\n";

	protected override Size DefaultSize{
		get{
			return new Size(600, 450);
		}
	}

	public PaGernateVB6Project2016CS20(){
		this.Text = "PaGernateVB6Project2016CS20";
		this.StartPosition = FormStartPosition.CenterScreen;

		textbox1.Size = new Size(210, 24);
		textbox1.Location = new Point(0, 10);
		textbox1.Text = m_strProjectName__conf;

		textbox2.Size = new Size(ClientRectangle.Width, ClientRectangle.Height - 40);
		textbox2.Location = new Point(0, 40);
		textbox2.Multiline = true;
		textbox2.Text = m_strProjectName__conf;
		textbox2.ScrollBars = ScrollBars.Vertical;

		button1.Size = new Size(75, 24);
		button1.Location = new Point(230, 10);
		button1.Text = "Gernate";

		button1.Click +=new EventHandler(this.Button1_Click);
		this.Resize += new EventHandler(PaGernateVB6Project2016CS20_Resize);

		this.Controls.Add(textbox1);
		this.Controls.Add(button1);
		this.Controls.Add(textbox2);
	}

	private void PaGernateVB6Project2016CS20_Resize(object sender, EventArgs e){
		if(ClientRectangle.Width>0 && ClientRectangle.Height>0){
			textbox2.Size = new Size(ClientRectangle.Width, ClientRectangle.Height - 40);
		}
	}

	private void Button1_Click(object sender, EventArgs e){
		string m_strProjName = textbox1.Text;
		string m_strFolder = m_strBaseFolder + m_strProjName + ".vb6";
		if(!Directory.Exists(m_strFolder)){
			Directory.CreateDirectory(m_strFolder);
		}
		string m_strBuffer = _BytesToString((byte[])(rm.GetObject("template-vbp.txt")));
		string m_strFile = m_strFolder + "\\" + m_strProjName + ".vbp";
		m_strBuffer = m_strBuffer.Replace("%__ProjectName__%", m_strProjName);
		using (StreamWriter sw = File.CreateText(m_strFile))
        {
            sw.Write(m_strBuffer);
            sw.Close();
        }

		m_strBuffer = _BytesToString((byte[])(rm.GetObject("template-frm.txt")));
		m_strBuffer = m_strBuffer.Replace("%__ProjectName__%", m_strProjName);
		textbox2.Text = m_strBuffer;
		m_strFile = m_strFolder + "\\frm" + m_strProjName + ".frm";
		using (StreamWriter sw = File.CreateText(m_strFile))
		{
			sw.Write(m_strBuffer);
			sw.Close();
		}

		m_strBuffer = _BytesToString((byte[])(rm.GetObject("template-bas.txt")));
		m_strBuffer = m_strBuffer.Replace("%__ProjectName__%", m_strProjName);
		m_strFile = m_strFolder + "\\bas" + m_strProjName + ".bas";
		using (StreamWriter sw = File.CreateText(m_strFile))
		{
			sw.Write(m_strBuffer);
			sw.Close();
		}

		m_strFile = m_strFolder + "\\" + m_strProjName + ".RES";
		using(BinaryWriter bw1 = new BinaryWriter(File.Open(m_strFile, FileMode.Create))) {
            bw1.Write((byte[])(rm.GetObject("template-res.bin")));
		}
	}

	private string _BytesToString(byte[] bytes1){
		return System.Text.Encoding.Default.GetString(bytes1);
	}

	public static void Main(string[] args){
		Application.Run(new PaGernateVB6Project2016CS20());
	}
}