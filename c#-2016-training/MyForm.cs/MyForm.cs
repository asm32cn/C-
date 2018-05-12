using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

public class MyForm : Form{
	private TextBox box;
	private Button button;
	public MyForm() : base(){
		this.Text = "MyForm";
		this.StartPosition = FormStartPosition.CenterScreen;
		box =new TextBox();
		box.BackColor = System.Drawing.Color.Cyan;
		box.Size =new Size(100,100);
		box.Location =new Point(50,50);
		box.Text ="Hello";
		button =new Button();
		button.Location =new Point(50,100);
		button.Text ="Click Me";
		// To wire the event, create
		// a delegate instance and add it to the Click event. 
		button.Click +=new EventHandler(this.Button_Click);
		Controls.Add(box);
		Controls.Add(button);
	}
	// The event handler.
	private void Button_Click(object sender, EventArgs e){
		box.BackColor = System.Drawing.Color.Green;
	}
	// The STAThreadAttribute indicates that Windows Forms uses the
	// single-threaded apartment model. 
	[STAThreadAttribute]
	public static void Main(string[] args){
		Application.Run(new MyForm());
	}
}