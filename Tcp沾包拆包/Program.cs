namespace TcpDipAndUnpack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var task = Task.Run(TcpFixedLengthReceiver.ReceiveFixedLengthData);
            var run = Task.Run(TcpFixedLengthSender.SendFixedLengthData);
            if (task.Status == TaskStatus.Canceled)
            {
                task.Start();
            }
            if (run.Status == TaskStatus.Canceled)
            {
                run.Start();
            }
            
            
        }
    }
}
