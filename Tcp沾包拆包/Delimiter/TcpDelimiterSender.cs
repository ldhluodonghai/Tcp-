using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpDipAndUnpack.Delimiter
{
    internal class TcpDelimiterSender
    {
        private const string Delimiter = "|#|";
        private const string ServerIP = "127.0.0.1";
        private const int ServerPort = 8889;

        public static void SendDataWithDelimiter()
        {
            try
            {                    
                while (true)
                {
                    // 创建TCP套接字
                    TcpClient client = new TcpClient();
                    client.Connect(ServerIP, ServerPort);

                    NetworkStream stream = client.GetStream();
                    string[] messages = { "message1", "message2", "message3" };
                    foreach (var m in messages)
                    {
                        Console.WriteLine($"send: {m}");
                    }
                    foreach (string msg in messages)
                    {
                        string dataToSend = msg + Delimiter;
                        byte[] data = Encoding.ASCII.GetBytes(dataToSend);
                        stream.Write(data, 0, data.Length);
                    }
                    stream.Close();
                    client.Close();
                    Thread.Sleep(1000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"发送数据时出错: {e.Message}");
            }
        }
    }
}
