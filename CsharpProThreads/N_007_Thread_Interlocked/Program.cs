using System;
using System.Globalization;
using System.Linq;
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
        
        /// <summary>
        /// Объект блокировки
        /// </summary>
        private static object block = new object();

        private static void Procedure()
        {
            // Увеличение счетчика

            for (int i = 0; i < 1_000_000; i++)
            {
                // 1.
                // counter++;
                
                // 2.
                lock (block)
                {
                    counter++;
                }
                
                // 3.
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
            
            var nfi = new NumberFormatInfo();
            nfi.NumberGroupSeparator = " "; // set the group separator to a space
            
            Console.WriteLine($"Реальное значение счетчика = {counter.ToString("N2", nfi)}");
            
            Console.ReadKey();
        }
    }
}

// Результат

// 1. Без простой вариант
// Ожидаемое значение счетчика = 10 000 000
// Реальное значение счетчика = 3 946 430.00

// 2. Критическая секция