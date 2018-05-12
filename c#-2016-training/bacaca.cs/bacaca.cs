using System;

public class bacaca{
	public static bool A(){
		Console.Write("A");
		return true;
	}
	public static void B(){
		Console.Write("B");
	}
	public static void Main(string[] args){
		int i=0;
		Console.Write("ºº×Ö");
		for(B();  A() & i<2  ; i++){
			Console.Write("C");
		}
		Console.ReadKey();
	}
}