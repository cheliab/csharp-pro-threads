using System;
using System.Threading;

// Статические значения общие для потоков
namespace Thread_003
{
    class Program
    {
        // Чтобы сделать статическое значение индивидуальны для потока нужно добавить атрибут
        // [ThreadStatic] // Используется Thread local storage (TLS)
        private static int counter;

        private static void Method()
        {
            if (counter < 10)
            {
                counter++; // увиличиваем общее значение

                Console.WriteLine($"{counter} - СТАРТ --- {Thread.CurrentThread.GetHashCode()}");

                // Рекурсивный вызов метода в новых потоках
                Thread thread = new Thread(Method);
                thread.Start();
                thread.Join();
            }

            Console.WriteLine($"Поток {Thread.CurrentThread.GetHashCode()} завершился");
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Первичный поток начало работы");
            
            // Запускаем первый поток рекурсии 
            Thread thread = new Thread(Method);
            thread.Start();
            thread.Join();
            
            Console.WriteLine("Первичный поток завершил работу");
            
            Console.ReadLine();
        }
    }
}

// Результат:
// Первичный поток начало работы
// 1 - СТАРТ --- 4
// 2 - СТАРТ --- 5
// 3 - СТАРТ --- 6
// 4 - СТАРТ --- 7
// 5 - СТАРТ --- 8
// 6 - СТАРТ --- 9
// 7 - СТАРТ --- 10
// 8 - СТАРТ --- 11
// 9 - СТАРТ --- 12
// 10 - СТАРТ --- 13
// Поток 14 завершился
// Поток 13 завершился
// Поток 12 завершился
// Поток 11 завершился
// Поток 10 завершился
// Поток 9 завершился
// Поток 8 завершился
// Поток 7 завершился
// Поток 6 завершился
// Поток 5 завершился
// Поток 4 завершился
// Первичный поток завершил работу