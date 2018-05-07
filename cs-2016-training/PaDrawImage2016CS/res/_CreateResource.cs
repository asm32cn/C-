using System;
using System.Drawing;
using System.Resources;

class _CreateResource{
	public static void Main(){
		ResourceWriter rw = new ResourceWriter("PaDrawImage2016CS.resources");
		//Icon ico = new Icon("Demo.ico");
		Image imgBg = Image.FromFile("小老虎-2-3-2");

		rw.AddResource("bg.gif" , imgBg);
		rw.Generate();
		rw.Close();
	}
}