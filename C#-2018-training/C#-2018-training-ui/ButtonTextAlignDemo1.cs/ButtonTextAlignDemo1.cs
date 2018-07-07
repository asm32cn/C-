// ButtonTextAlignDemo1.cs

using System;
using System.Drawing;
using System.Windows.Forms;

class ButtonTextAlignDemo1 : Form{
	private int nCommandCount = 7;
	private Button[] btnCommands = new Button[7];
	private TableLayoutPanel tlp = new TableLayoutPanel();
	private AnchorStyles as1 = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));

	private string[] A_strButtonsText = {
		"TIOBE Index for January 2018.html",
		"TIOBE Index for February 2018.html",
		"TIOBE Index for March 2018.html",
		"TIOBE Index for April 2018.html",
		"TIOBE Index for May 2018.html",
		"TIOBE Index for June 2018.html",
		"TIOBE Index for July 2018.html"
	};

	protected override Size DefaultSize {
		get { return new Size(500, 300); }
	}

	public ButtonTextAlignDemo1(){
		this.Text = "ButtonTextAlignDemo1.cs";
		this.StartPosition = FormStartPosition.CenterScreen;
		this.MinimumSize = new Size(500, 300);
		this.Padding = new Padding(10);

		initUI();
	}

	private void initUI(){
		this.tlp.SuspendLayout();
		this.SuspendLayout();

		this.tlp.Anchor = as1;
		this.tlp.ColumnCount = 1;
		this.tlp.RowCount = nCommandCount;
		this.tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

		for(int i = 0; i < nCommandCount; i++){
			this.tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
		}

		for(int i = 0; i < nCommandCount; i++){
			btnCommands[i] = new Button();
			// btnCommands[i].Text = string.Format("ButtonTextAlignDemo1.cs - {0}", i);
			btnCommands[i].Text = A_strButtonsText[i];
			btnCommands[i].Anchor = as1;
			btnCommands[i].Dock = DockStyle.Fill;
			btnCommands[i].TextAlign = ContentAlignment.MiddleLeft;

			this.tlp.Controls.Add(btnCommands[i]);
		}

		this.Controls.Add(this.tlp);
		this.tlp.ResumeLayout(false);
		this.tlp.PerformLayout();
		this.ResumeLayout(false);
		this.tlp.Dock = DockStyle.Fill;
	}

	public static void Main(string[] args){
		Application.Run(new ButtonTextAlignDemo1());
	}
}