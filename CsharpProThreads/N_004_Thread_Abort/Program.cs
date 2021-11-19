using System;
using System.Threading;

namespace Thread_Abort_004
{
    /// <summary>
    /// Прерывание потока - Abort
    /// </summary>
    class Program
    {
        /// <summary>
        /// Метод второго потока
        /// </summary>
        private static void Procedure()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            while (true)
            {
                try
                {
                    Thread.Sleep(10);
                    Console.Write(".");
                }
                catch (ThreadAbortException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    
                    // Попытка 'проглотить' исключение и продолжить выполняться в данном циклею
                    // т.е. вернуться в цикл и продолжить выполнять counter
                    
                    Console.WriteLine("\nThreadAbortException");

                    for (int i = 0; i < 160; i++)
                    {
                        Thread.Sleep(20);
                        Console.Write(".");
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                } 
                // После блока catch повторно бросается исключение,
                // которое убивает поток и не дает вернуться в бесконеный цикл 
                
            }
            Console.WriteLine("+++++ НЕ ВЫПОЛНИТСЯ +++++");
        }
        
        static void Main(string[] args)
        {
            Thread thread = new Thread(Procedure);
            thread.Start();
            
            // Немного выполняется основной поток
            Thread.Sleep(2000);

            // Прерываем поток
            thread.Abort(); // помечен как устаревший

            // Ждем завершения потока
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