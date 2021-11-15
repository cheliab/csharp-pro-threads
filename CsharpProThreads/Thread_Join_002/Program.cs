using System;
using System.Threading;

namespace Thread_Join_002
{
    class Program
    {
        /// <summary>
        /// Вывод символов
        /// </summary>
        /// <param name="chr">Символ</param>
        /// <param name="count">Количество</param>
        /// <param name="color">Цвет</param>
        static void WriteChar(char chr, int count, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            for (int i = 0; i < count; i++)
            {
                Thread.Sleep(20);
                Console.Write(chr);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void MethodThread3()
        {
            Console.WriteLine($"Третий поток № {Thread.CurrentThread.GetHashCode()}");
            WriteChar('3', 80, ConsoleColor.Yellow);
            Console.WriteLine("Третий поток завершился");
        }
        
        static void MethodThread2()
        {
            Console.WriteLine($"Вторичный поток № {Thread.CurrentThread.GetHashCode()}");
            WriteChar('2', 80, ConsoleColor.Blue);

            var thread = new Thread(MethodThread3);
            thread.Start();
            thread.Join(); // Второй поток ждет пока завершится 3 поток
            
            WriteChar('2', 80, ConsoleColor.Blue);
            Console.WriteLine("Вторичный поток завершился");
        }
        
        static void Main(string[] args)
        {
            
            
            Console.ReadLine();
        }
    }
}