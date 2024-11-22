using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpDipAndUnpack.FixedLeng
{
    public class TcpFixedLengthSender
    {
        private const int FixedLength = 10;
        private const string ServerIP = "127.0.0.1";
        private const int ServerPort = 8888;

        public static void SendFixedLengthData()
        {

            try
            {

                while (true)
                {// 创建TCP套接字
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
                        // 如果消息长度小于固定长度，进行填充
                        if (msg.Length < FixedLength)
                        {
                            msg.PadRight(FixedLength);
                        }

                        byte[] data = Encoding.ASCII.GetBytes(msg);
                        stream.Write(data, 0, data.Length);
                    }

                    stream.Close();
                    client.Close();
                    Thread.Sleep(3000);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"发送数据时出错: {e.Message}");
            }


        }
    }
}
