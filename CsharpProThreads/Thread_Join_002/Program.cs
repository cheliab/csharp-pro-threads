using System;
using System.Threading;

// Три пока с использованием Join
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
            Console.Write($"{Environment.NewLine}");
            Console.WriteLine($"Третий поток № {Thread.CurrentThread.GetHashCode()}");
            
            WriteChar('3', 80, ConsoleColor.Yellow);
            
            Console.Write($"{Environment.NewLine}");
            Console.WriteLine("Третий поток завершился");
        }
        
        static void MethodThread2()
        {
            Console.Write($"{Environment.NewLine}");
            Console.WriteLine($"Второй поток № {Thread.CurrentThread.GetHashCode()}");
            
            WriteChar('2', 80, ConsoleColor.Blue);

            // Создаем третий поток
            var thread = new Thread(MethodThread3);
            thread.Start();
            thread.Join(); // Второй поток ждет пока завершится 3 поток
            
            WriteChar('2', 80, ConsoleColor.Blue);
            
            Console.Write($"{Environment.NewLine}");
            Console.WriteLine("Вторичный поток завершился");
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine($"Первый поток № {Thread.CurrentThread.GetHashCode()}");
            
            WriteChar('1', 80, ConsoleColor.Green);

            Thread thread = new Thread(MethodThread2);
            thread.Start();
            thread.Join();
            
            WriteChar('1', 80, ConsoleColor.Green);
            
            Console.Write($"{Environment.NewLine}");
            Console.WriteLine("Первый поток завершился");
            
            Console.ReadLine();
        }
    }
}