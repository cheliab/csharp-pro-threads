using System;
using System.Threading;

namespace N_005_Thread_CancellationToken
{
    /// <summary>
    /// Использование CancellationToken для прерывания потока
    /// </summary>
    class Program
    {
        private static void Procedure(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            
            Console.ForegroundColor = ConsoleColor.Green;

            while (true)
            {
                if (token.IsCancellationRequested)
                    break;
                
                Thread.Sleep(10);
                Console.Write(".");
            }
        }
        
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            Thread thread = new Thread(Procedure);
            thread.Start(cts.Token);
            
            Thread.Sleep(2000);
            
            cts.Cancel();

            thread.Join();
            
            Console.ForegroundColor = ConsoleColor.White;

            while (true)
            {
                Thread.Sleep(20);
                Console.Write("-");
            }
            
            Console.ReadLine();
        }
    }
}