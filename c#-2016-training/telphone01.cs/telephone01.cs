class telephone01{
	public static void Main(string[] args){
		int[] arr = new int[]{8, 2, 1, 0, 3};
		int[] index = new int[]{2, 0, 3, 2, 4, 0, 1, 3, 2, 3, 3};
		string tel = "";
		foreach(int i in index){
			tel += arr[i];
		}
		System.Console.Write("联系方式:"+tel);
	}
}
