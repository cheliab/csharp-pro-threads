using System;

namespace N_009_Thread_Monitor
{
    class Program
    {
        /// <summary>
        /// Объект блокировки
        /// </summary>
        private static object block = new object();
        
        static void Main(string[] args)
        {
            Console.ReadKey();
        }
    }
}