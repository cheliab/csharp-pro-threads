using System;
using System.Threading;

// Использование Join в потоках
namespace CsharpProThreads
{
    class Program
    {
        static void Function()
        {
            Console.WriteLine($"ID Вторичного потока: {Thread.CurrentThread.ManagedThreadId}");
            Console.ForegroundColor = ConsoleColor.Yellow;

            for (int i = 0; i < 160; i++)
            {
                Thread.Sleep(20);
                Console.Write(".");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Вторичный поток завершился.");
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine($"ID Первичного потока: {Thread.CurrentThread.GetHashCode()}");

            // Создаем поток
            Thread thread = new Thread(Function);
            thread.Start();

            // Блокируем первичный поток, пока не завершится второй поток
            // thread.Join();

            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < 160; i++)
            {
                Thread.Sleep(20);
                Console.Write("-");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Первичный поток завершился");

            Console.ReadLine();
        }
    }
}

// Результат:
// ID Первичного потока: 1
// ID Вторичного потока: 4
// ................................................................................................................................................................Вторичный поток завершился.
// ----------------------------------------------------------------------------------------------------------------------------------------------------------------Первичный поток завершился

// Результат работы без Join:
// ID Первичного потока: 1
// -..-.--.-.-.-.-.-.-.-.-.-.-..-.--..-.--..--.-.-..--.-..-.-.-.-.--.-..--..-.-.-.-.-.-.-.-.-.-.--..--..--..--.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-..--..-.--..--..-.--.-..--..--..-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.-.--..-.--..-.--..
// --.-.-.-..-.-.--..-.-.-.-.-.-.-.-.-.--.-..--.-..-.--.-..--.-.-.-.-..-.-.-.-.-.-.-.-.-.-.-.Вторичный поток завершился.
// -Первичный поток завершился
