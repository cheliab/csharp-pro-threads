using System;
using System.Threading;

namespace N_004_Thread_Abort_03_OperationCanceledException
{
    class Program
    {
        private static void Procedure(object obj)
        {
            CancellationToken token = (CancellationToken)obj;
            
            while (true)
            {
                try
                {
                    token.ThrowIfCancellationRequested();
                    
                    Thread.Sleep(10);
                    Console.Write(".");
                }
                catch (OperationCanceledException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    
                    Console.WriteLine("\nOperationCanceledException");

                    for (int i = 0; i < 160; i++)
                    {
                        Thread.Sleep(20);
                        Console.Write(".");
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                }
                // Отличие от Abort
                // Тут продолжит выполняться цикл
            }
            Console.WriteLine("++++++ НЕ ВЫПОЛНИТСЯ +++++++");
        }
        
        static void Main(string[] args)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            
            Thread thread = new Thread(Procedure);
            thread.Start(tokenSource.Token);
            
            Thread.Sleep(2000);
            
            tokenSource.Cancel();

            Console.ReadLine();
        }
    }
}