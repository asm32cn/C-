// socket-webserver-demo-2.cs

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

class socket_webserver_demo_2{
	public static void Main(string[] args){
		//IPAddress address = IPAddress.Loopback;	//  取得本机的loopback地址，即127.0.0.1
		//IPEndPoint endPoint = new IPEndPoint(address, 49152);	// 创建可访问的断电,49152表示端口号,如果设置为0表示一个空闲的端口号
		IPAddress address = IPAddress.Parse("0.0.0.0");
		IPEndPoint endPoint = new IPEndPoint(address, 8001); // 创建可访问的端点，8001表示端口号
		// 创建 socket ，使用IPv4地址，数字通信类型为字节流，TCP协议
		Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		socket.Bind(endPoint); //将 socket 绑定到一个端点上
		socket.Listen(5); // 设置连接队列的长度
		Console.WriteLine("开始监听，端口号: {0}", endPoint.Port);
		while(true){
			Socket client = socket.Accept();	// 开始监听，这个方法会阻塞线程的执行，直到接受到一个客户端的请求连接
			Console.WriteLine(client.RemoteEndPoint); // 输出客户端的地址
			byte[] buffer = new byte[4096]; // 准备读取客户端请求的数据，读取的数据将保存在一个数组中
			int length = client.Receive(buffer, 4096, SocketFlags.None);	// 接受数据
			// 将请求数据翻译为 utf-8
			System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
			string requestString = utf8.GetString(buffer, 0, length);
			Console.WriteLine(requestString);	// 显示请求
			// 回应的状态行
			string statusLine = "HTTP/1.1 200 OK\r\n";
			byte[] statusLineBytes = utf8.GetBytes(statusLine);
			// 准备发送回客户端的网页
			string responseBody = "<html><head><title>From Socket Server</title></head><body><h1>Hello socket!</h1></body></html>";
			byte[] responseBodyBytes = utf8.GetBytes(responseBody);
			// 回应的头部
			string responseHeader = string.Format("Content-Type: text/html; charset=UTF-8\r\nContent-Length: {0}\r\n", responseBody.Length);
			byte[] responseHeaderBytes = utf8.GetBytes(responseHeader);

			// 向客户端发送状态信息
			client.Send(statusLineBytes);
			// 向客户端发送回应头
			client.Send(responseHeaderBytes);
			// 头部与内容的分隔行
			client.Send(new byte[]{13, 10});
			// 像客户端发送内容部分
			client.Send(responseBodyBytes);

			// 断开与客户端的连接
			client.Close();
			if(Console.KeyAvailable)
				break;
		}
		socket.Close();
	}
}