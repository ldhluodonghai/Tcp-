
using System.Net.Sockets;
using System.Text;


namespace TcpDipAndUnpack.FixedLeng
{
    public class TcpFixedLengthSender
    {
        private const int FixedLength = 10;
        private const string ServerIP = "127.0.0.1";
        private const int ServerPort = 8880;

        public static void SendFixedLengthData()
        {
           
            try
            {
                //var client = new TcpClient();
                //bool needToReconnect = true;
                while (true)
                {
                    //if (needToReconnect)
                    //{
                    //    client.Connect(ServerIP, ServerPort);
                    //    needToReconnect = false;
                    //}
                    // 创建TCP套接字
                    var client = new TcpClient();
                    client.Connect(ServerIP, ServerPort);
                    NetworkStream stream = client.GetStream();
                    string[] messages = { "message1", "message2l", "message3" };
                    foreach (var m in messages)
                    {
                        Console.WriteLine($"send: {m}");
                    }

                    foreach (string msg in messages)
                    {
                        string send = "";
                        // 如果消息长度小于固定长度，进行填充
                        if (msg.Length < FixedLength)
                        {
                           send = msg.PadRight(FixedLength,'o');
                        }

                        byte[] data = Encoding.ASCII.GetBytes(send);
                        stream.Write(data, 0, data.Length);
                    }
                    stream.Close();
                    client.Close();
                    //// 这里假设根据某些条件判断是否需要重新连接，比如接收到服务器特定的断开连接通知等
                    //// 以下是示例的简单判断逻辑，你可以根据实际情况修改

                    //while (client.Connected)
                    //{
                    //    client.Close();
                    //}

                    //needToReconnect = true;

                }

            }
            catch (Exception e)
            {
               
                Console.WriteLine($"发送数据时出错: {e.Message}");
            }


        }
    }
}
