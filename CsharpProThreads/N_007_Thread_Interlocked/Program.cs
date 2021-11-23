using System;
using System.Threading;

// Interlocked - Атомарные операции с переменными, доступными нескольким потокам
namespace N_007_Thread_Interlocked
{
    class Program
    {
        /// <summary>
        /// Счетчик запущенных потоков
        /// </summary>
        private static long counter;
        
        private static object block = new object();

        private static void Procedure()
        {
            // Увеличение счетчика

            for (int i = 0; i < 1_000_000; i++)
            {
                // 1.
                counter++;
                
                // 2.
                // lock (block)
                // {
                //     counter++;
                // }
                
                // Interlocked.Increment(ref counter);
            }
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Ожидаемое значение счетчика = 10 000 000");

            Thread[] threads = new Thread[10];
            
            for (int i = 0; i < 10; i++)
                (threads[i] = new Thread(Procedure)).Start();

            for (int i = 0; i < 10; i++)
                threads[i].Join();
            
            Console.WriteLine($"Реальное значение счетчика = {counter}");
            
            Console.ReadKey();
        }
    }
}