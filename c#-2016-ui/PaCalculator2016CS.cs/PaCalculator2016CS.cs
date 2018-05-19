// PaCalculator2016CS.cs
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;

public class PaCalculator2016CS : Form {

	private ResourceManager rm = new ResourceManager("PaCalculator2016CS",
		System.Reflection.Assembly.GetExecutingAssembly());

	private TableLayoutPanel tlp = null;
	private TextBox txtDisplay = null;
	private Button[] buttons = new Button[20];
	private Font objFont = new Font("Arial", 16);

	private string[] A_strKeys = new string[]{
		"7", "8", "9", "*", "←",
		"4", "5", "6", "/", "C",
		"1", "2", "3", "-", "=",
		"0", ".", "+", 
	};

	protected override Size DefaultSize{
		get{
			return new Size(300, 300);
		}
	}

	public PaCalculator2016CS(){
		this.Text = "C# 计算器";
		this.StartPosition = FormStartPosition.CenterScreen;
		this.MinimumSize = new Size(300, 300);

		this.Icon = (Icon)(rm.GetObject("this.ico"));

		AnchorStyles as1 = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));

		this.tlp = new TableLayoutPanel();
		this.tlp.SuspendLayout();
		this.SuspendLayout();

		this.tlp.Anchor = as1;
		this.tlp.ColumnCount = 5;
		this.tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
		this.tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
		this.tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
		this.tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
		this.tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

		this.tlp.RowCount = 5;
		this.tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
		this.tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
		this.tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
		this.tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
		this.tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

		this.txtDisplay = new TextBox();
		this.txtDisplay.AutoSize = false;
		this.txtDisplay.TextAlign = HorizontalAlignment.Right;
		this.txtDisplay.Font = objFont;
		//this.txtDisplay.Anchor = as1;
		this.txtDisplay.Dock = DockStyle.Fill;
		this.tlp.SetColumnSpan(this.txtDisplay, 5);
		this.tlp.Controls.Add(this.txtDisplay, 0, 0);

		int nRow = 1;
		int nCol = 0;
		for (int i=0; i<A_strKeys.Length; i++){
			if (i==5 || i==10||i==15){
				nRow++;
				nCol = 0;
			}
			buttons[i] = new Button();
			buttons[i].Font = objFont;
			buttons[i].Text = this.A_strKeys[i];
			if (nRow==3 && nCol==4){
				this.tlp.SetRowSpan(buttons[i], 2);
			}else if(nRow==4 && nCol==0){
				this.tlp.SetColumnSpan(buttons[i], 2);
			}
			buttons[i].Anchor = as1;
			this.tlp.Controls.Add(buttons[i], nCol, nRow);
			if(nRow==4 && nCol==0) nCol++;
			nCol++;
			
		}

		this.Controls.Add(this.tlp);
		this.Padding = new Padding(10);
		this.tlp.ResumeLayout(false);
		this.tlp.PerformLayout();
		this.ResumeLayout(false);
		this.tlp.Dock = DockStyle.Fill;

		this.txtDisplay.Text = "3.14159265358979323846";

	}

	public static void Main(string[] args){
		Application.Run(new PaCalculator2016CS());
	}
}
