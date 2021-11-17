using System;
using System.Threading;

namespace Thread_Abort_004
{
    /// <summary>
    /// Прерывание потока
    /// </summary>
    class Program
    {
        private static void Procedure()
        {
            
        }
        
        static void Main(string[] args)
        {
            Thread thread = new Thread(Procedure);
            thread.Start();
            
            Thread.Sleep(2000);
            
            thread.Abort(); // помечен как устаревший

            thread.Join();
            
            Console.ReadLine();
        }
    }
}