using System;
using System.Threading;

// Пример использования приоритетов в потоках
namespace N_006_Thread_Priority
{
    class PriorityTest
    {
        public bool stop = false;

        public void Method()
        {
            Console.WriteLine("Поток {0,3} с приоритетом {1,11} начал работу",
                Thread.CurrentThread.ManagedThreadId,
                Thread.CurrentThread.Priority);

            long count = 0;

            while (!stop)
                count++;
            
            Console.WriteLine("Поток {0,3} с приоритетом {1,11} завершился. Count = {2,13}",
                Thread.CurrentThread.ManagedThreadId,
                Thread.CurrentThread.Priority,
                    count.ToString("N0"));
        }
    }
    
    class Program
    {
        private static int numberOfThreads = 11;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key...");
            Console.ReadKey();
            
            Console.WriteLine("Приоритет первичного потока по умолчанию: {0}",
                Thread.CurrentThread.Priority); // обычный приоритет Normal

            PriorityTest priorityTest = new PriorityTest();

            Thread[] threads = new Thread[numberOfThreads];

            for (int i = 0; i < numberOfThreads; i++)
                threads[i] = new Thread(priorityTest.Method);
            
            // Устанавливаем пироритеты потокам

            threads[0].Priority = ThreadPriority.Lowest;

            for (int i = 1; i < numberOfThreads; i++)
                threads[i].Priority = ThreadPriority.Highest;
            
            // Закуск 1-го потока с низким приоритетом
            threads[0].Start();
            
            // Немного ждем перед запуском высоко приоритетных потоков
            Thread.Sleep(1000);
            
            // Запуск 8-ми потоков с высоким приоритетом
            for (int i = 1; i < numberOfThreads; i++)
                threads[i].Start();
            
            // Даем потокам выполняться 10 секунд
            Thread.Sleep(10_000);
            
            Console.WriteLine("Первичный поток проснулся и втиснулся в выполнение потоков");

            // Остановка работы всех потоков
            priorityTest.stop = true;
            
            Console.ReadKey();
        }
    }
}