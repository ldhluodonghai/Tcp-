using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace TcpDipAndUnpack.Length
{
    public class TcpLengthReceiver
    {
        private const string ServerIP = "127.0.0.1";
        private const int ServerPort = 8890;

        public static void ReceiveDataWithLength()
        {
            try
            {
                // 创建TCP套接字并监听端口
                TcpListener server = new TcpListener(IPAddress.Parse(ServerIP), ServerPort);
                server.Start();

                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    byte[] lengthData = new byte[4];
                    int bytesRead = stream.Read(lengthData, 0, 4);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    int length = BitConverter.ToInt32(lengthData, 0);
                    byte[] data = new byte[length];
                    int totalBytesRead = 0;
                    while (totalBytesRead < length)
                    {
                        int bytesThisRead = stream.Read(data, totalBytesRead, length - totalBytesRead);
                        if (bytesThisRead == 0)
                        {
                            break;
                        }

                        totalBytesRead += bytesThisRead;
                    }

                    if (totalBytesRead == length)
                    {
                        string receivedData = Encoding.ASCII.GetString(data);
                        Console.WriteLine($"Received: {receivedData}");
                    }
                }

                stream.Close();
                client.Close();
                server.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine($"接收数据时出错: {e.Message}");
            }
        }
    }
}
