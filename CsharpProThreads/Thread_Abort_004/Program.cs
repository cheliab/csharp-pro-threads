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
            
            Console.ReadLine();
        }
    }
}