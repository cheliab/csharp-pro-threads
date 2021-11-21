using System;
using System.Threading;

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
        static void Main(string[] args)
        {
            
            Console.ReadKey();
        }
    }
}