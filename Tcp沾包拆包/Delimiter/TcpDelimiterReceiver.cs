using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace TcpDipAndUnpack.Delimiter
{
    public class TcpDelimiterReceiver
    {
        private const string Delimiter = "|#|";
        private const string ServerIP = "127.0.0.1";
        private const int ServerPort = 8889;

        public static void ReceiveDataWithDelimiter()
        {
            try
            {
                // 创建TCP套接字并监听端口
                TcpListener server = new TcpListener(IPAddress.Parse(ServerIP), ServerPort);
                server.Start();

                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                StringBuilder buffer = new StringBuilder();

                while (true)
                {
                    byte[] data = new byte[1024];
                    int bytesRead = stream.Read(data, 0, 1024);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    buffer.Append(Encoding.ASCII.GetString(data, 0, bytesRead));
                    string[] messages = buffer.ToString().Split(Delimiter);
                    for (int i = 0; i < messages.Length - 1; i++)
                    {
                        Console.WriteLine($"Received: {messages[i]}");
                    }

                    buffer.Clear();
                    buffer.Append(messages[messages.Length - 1]);
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
