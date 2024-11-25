using System.Net.Sockets;
using System.Net;
using System.Text;


namespace TcpDipAndUnpack.FixedLeng
{
    public class TcpFixedLengthReceiver
    {
        private const int FixedLength = 10;
        private const string ServerIP = "127.0.0.1";
        private const int ServerPort = 8880;

        public static void ReceiveFixedLengthData()
        {
            try
            {
                TcpListener server = new TcpListener(IPAddress.Parse(ServerIP), ServerPort);
                server.Start();
                // 创建TCP套接字并监听端口
                while (true)
                {

                    TcpClient client = server.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    while (true)
                    {
                        byte[] buffer = new byte[FixedLength];
                        int bytesRead = stream.Read(buffer, 0, FixedLength);
                        if (bytesRead == 0)
                        {
                            break;
                        }
                        string data = Encoding.ASCII.GetString(buffer).Trim();
                        Console.WriteLine($"Received: {data}");
                    }
                    stream.Close();
                    client.Close();
                    //server.Stop();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"接收数据时出错: {e.Message}");
            }


        }
    }
}
