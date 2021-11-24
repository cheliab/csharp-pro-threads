using System;
using System.IO;
using System.Threading;

// Ручная реализация блокировки
namespace N_008_Thread_Interlocked_SpinLock
{
    public class SpinLock
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
            Interlocked.Exchange(ref block, 0);
        }
    }

    /// <summary>
    /// Логика работы конструкции - lock 
    /// </summary>
    public class SpinLockManager : IDisposable
    {
        private SpinLock _block;

        public SpinLockManager(SpinLock block)
        {
            _block = block;
            
            _block.Enter();
        }
        
        public void Dispose()
        {
            _block.Exit();
        }
    }
    
    class Program
    {
        private static Random random = new Random();
        private static SpinLock block = new SpinLock(10); // Интервал между проверками 10 мс.

        private static FileStream stream = File.Open("log.txt", FileMode.Create, FileAccess.Write, FileShare.None);
        private static StreamWriter writer = new StreamWriter(stream);

        /// <summary>
        /// Метод для потока
        /// </summary>
        /// <remarks>
        /// Кажись тут не правильный пример, так как поток сразу разблокируется после записи в файл и имитакция работы по факту не защищена
        /// Х.з зачем такой сложный пример с файлами (вообще не наглядный)
        /// </remarks>
        private static void Function()
        {
            using (new SpinLockManager(block)) // вызывается block.Enter();
            {
                writer.WriteLine("Поток {0} запускается.", Thread.CurrentThread.GetHashCode());
                writer.Flush(); // Запись в файл и очистка буфера
            } // вызывается block.Exit();

            // Имитация работы
            int time = random.Next(10, 200);
            Thread.Sleep(time); // Ждем рандомное время

            using (new SpinLockManager(block)) // Enter
            {
                writer.WriteLine("Поток [{0}] завершается.", Thread.CurrentThread.GetHashCode());
                writer.Flush();
            } // Exit
        }

        /// <summary>
        /// Правильный вариант блокировки
        /// </summary>
        /// <remarks>
        /// При таком выполнении блок с полезной работой защищен
        /// т.к. последующие потоки будут сидеть в цикле пока первый поток не выполнится
        /// Хотя может я не до конца понял суть примера
        /// </remarks>
        private static void RightFunc()
        {
            using (new SpinLockManager(block)) // Enter
            {
                writer.WriteLine("Поток {0} запустился", Thread.CurrentThread.GetHashCode());
                writer.Flush();
                
                // Полезная работа
                int time = random.Next(10, 200);
                Thread.Sleep(time);

                writer.WriteLine("Поток [{0}] завершился.", Thread.CurrentThread.GetHashCode());
                writer.Flush();
            } // Exit
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Main - Start");
            
            Thread[] threads = new Thread[50];

            for (int i = 0; i < 50; i++)
            {
                threads[i] = new Thread(Function);
                threads[i].Start();
            }
            
            Console.WriteLine("Main - End");
            
            Console.ReadKey();
        }
    }
}