using System;
using System.Security;
using System.Threading;

namespace N_010_Thread_Lock
{
    class Program
    {
        private static object block = new object();
        private static int counter;
        private static Random random = new Random();

        private static void Function()
        {
            lock (block)
            {
                counter++;
            }

            int time = random.Next(1000, 12000);
            Thread.Sleep(time);

            lock (block)
            {
                counter--;
            }
        }

        private static void Report()
        {
            while (true)
            {
                int count;

                lock (block)
                {
                    count = counter;
                }
                
                Console.WriteLine("{0} потоков активно", count);
                Thread.Sleep(100);
            }
        }
        
        static void Main(string[] args)
        {
            var reporterThread = new Thread(Report)
            {
                IsBackground = true
            };
            reporterThread.Start();
            
            
            
        }
    }
}