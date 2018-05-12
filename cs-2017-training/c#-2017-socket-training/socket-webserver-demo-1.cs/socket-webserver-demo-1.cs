// socket-webserver-demo-1.cs
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Windows.Forms;

class socket_webserver_demo1 : Form {

	protected override System.Drawing.Size DefaultSize{
		get{
			return new System.Drawing.Size(600, 200);
		}
	}

	public socket_webserver_demo1(){

		this.Text = "webserver_demo1.cs (WebServer 0.0.0.0:8002)";
		this.StartPosition = FormStartPosition.CenterScreen;

		Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		socketWatch.Bind(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 8002));
		socketWatch.Listen(5); // 参数表示最多可容纳的等待接受的传入连接数，不包含已经建立连接的。

		Thread thread = new Thread(
			delegate(object obj){
				Socket socketListen = (Socket)obj;
				while (true){
					try{

						Console.WriteLine("Ready.........................");
						Socket socket = socketListen.Accept();
						byte[] data = new byte[1024 * 4]; // 浏览器发来的数据，限定为 4K。
						int length = socket.Receive(data, 0, data.Length, SocketFlags.None);

						int l;	// request length
						for(l = 0; l < 4096; l++){
							if(data[l] == 0) break;
						}
						byte[] bytesRequest = new byte[l];
						System.Buffer.BlockCopy(data, 0, bytesRequest, 0, l);

						String strRequest = System.Text.Encoding.Default.GetString( bytesRequest );
						bool isRequestIndex = (data[5] == 32);

						Console.WriteLine( "Request Length: " + l + " ......");
						Console.WriteLine( strRequest );

						string strResponse;
						if(isRequestIndex){
							// byte[] body = Encoding.UTF8.GetBytes("<html>\n<head><title>From Socket WebServer</title></head>\n" +
							// 	"<body>\n<h1>Hello socket.</h1>\n<p>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") +
							// 	"</p>\n</body>\n</html>");

							// byte[] head = Encoding.UTF8.GetBytes("HTTP/1.1 200 OK\r\n" +
							// 	"Server: WebServer.C#\r\n" +
							// 	"Content-Type: text/plain; charset=utf-8\r\n" +
							// 	"Content-Length: " + body.Length + "\r\n\r\n");

							// Console.WriteLine(body.Length);

							// byte[] bytesResponse = new byte[head.Length + body.Length];
							// System.Buffer.BlockCopy(head, 0, bytesResponse, 0, head.Length);
							// System.Buffer.BlockCopy(body, 0, bytesResponse, head.Length, body.Length);

							// // socket.Send(head);
							// // socket.Send(body);
							// socket.Send( bytesResponse );

							strResponse = "HTTP/1.1 200 OK\r\n" +
								"Server: WebServer.C#\r\n" +
								"Content-Type: text/html; charset=utf-8\r\n" +
								"Content-Length: 133\r\n\r\n" +
								"<html>\n<head><title>From Socket WebServer</title></head>\n" +
								"<body>\n<h1>Hello socket.</h1>\n<p>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") +
								"</p>\n</body>\n</html>";

						}else{

							strResponse = "HTTP/1.1 404 Not Found\r\n" +
								"Server: WebServer.C#\r\n" +
								"Content-Type: text/plain; charset=utf-8\r\n" +
								"Content-Length: 3\r\n\r\n404";

						}

						socket.Send( Encoding.UTF8.GetBytes(strResponse) );
						Console.WriteLine( strResponse );

						socket.Shutdown(SocketShutdown.Both);
						socket.Close();
						Console.WriteLine("Succeed.......................\n");
					}catch(Exception e){
						Console.WriteLine(e);
					}
				}
			});

		thread.IsBackground = true;
		thread.Start(socketWatch);
	}

	public static void Main(string[] args){
		Application.Run(new socket_webserver_demo1());
	}
}