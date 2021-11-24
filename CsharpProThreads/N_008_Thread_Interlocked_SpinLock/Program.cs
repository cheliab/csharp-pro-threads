using System;
using System.Threading;

// Ручная реализация блокировки
namespace N_008_Thread_Interlocked_SpinLock
{
    class SpinLock
    {
        /// <summary>
        /// Флаг [0 - блок свободен, 1 - блок занят]
        /// </summary>
        private int block;

        /// <summary>
        /// Интервал проверки переменной "block"
        /// </summary>
        private int wait;

        public SpinLock(int wait)
        {
            this.wait = wait;
        }

        /// <summary>
        /// Установить блокировку (аналог - Monitor.Enter())
        /// </summary>
        public void Enter()
        {
            // Метод CompareExchage() [ Алгоритм работы ]
            // 1. Сравнивает начальное значение первого аргумента с третьим аргументом.
            // 2. Если первый аргумент (ref block) равен третьему аргументу (0), то в первый аргумент записывается значение второго аргумента (1) 
            // 3. Иначе, если первый аргумент (ref block) не равен третьему аргументу (0), то первый аргумент остается прежним
            // 4. Возвращает начальное значение первого аргумента (ref block) (всегда).
            
            int result = Interlocked.CompareExchange(ref block, 1, 0);

            while (result == 1)
            {
                Thread.Sleep(wait);
                result = Interlocked.CompareExchange(ref block, 1, 0);
            }
        }

        /// <summary>
        /// Сбросить блокировку (аналог - Monitor.Exit())
        /// </summary>
        public void Exit()
        {
            
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