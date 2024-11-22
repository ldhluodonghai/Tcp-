using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpDipAndUnpack.Length
{
    internal class TcpLengthSender
    {
        private const string ServerIP = "127.0.0.1";
        private const int ServerPort = 8890;

        public static void SendDataWithLength()
        {
            try
            {
                // 创建TCP套接字
                TcpClient client = new TcpClient();
                client.Connect(ServerIP, ServerPort);

                NetworkStream stream = client.GetStream();
                string[] messages = { "message1", "message2", "message3" };

                foreach (string msg in messages)
                {
                    int length = msg.Length;
                    byte[] lengthData = BitConverter.GetBytes(length);
                    stream.Write(lengthData, 0, lengthData.Length);
                    byte[] data = Encoding.ASCII.GetBytes(msg);
                    stream.Write(data, 0, data.Length);
                }

                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"发送数据时出错: {e.Message}");
            }
        }
    }
}
