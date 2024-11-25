using TcpDipAndUnpack.Delimiter;
using TcpDipAndUnpack.FixedLeng;

namespace TcpDipAndUnpack
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //TcpFixedLengthReceiver.ReceiveFixedLengthData();
            //TcpFixedLengthSender.SendFixedLengthData();
            var task = Task.Run(TcpFixedLengthReceiver.ReceiveFixedLengthData);
            var run = Task.Run(TcpFixedLengthSender.SendFixedLengthData);
            //var task = Task.Run(TcpDelimiterReceiver.ReceiveDataWithDelimiter);
            //var run = Task.Run(TcpDelimiterSender.SendDataWithDelimiter);
            Console.ReadLine();
            //while (true)
            //{
            //    if (task.Status == TaskStatus.Canceled)
            //    {
            //        task.Start();
            //    }
            //    if (run.Status == TaskStatus.Canceled)
            //    {
            //        run.Start();
            //    }
            //}





        }
    }
}
