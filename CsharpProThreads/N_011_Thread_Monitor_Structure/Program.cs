using System;
using System.Runtime.InteropServices;
using System.Threading;

// Использование струкрур при работе с Monitor

// ! Нельзя использовать объекты блокировки структурного типа
// при работе с объектом класса - Monitor

// Lock - не работает корретно с объектами блокировки структурных типов,
// допускается использование объектов блокировки только ссылочных типов.

namespace N_011_Thread_Monitor_Structure
{
    class Program
    {
        /// <summary>
        /// Счетчик (разделяемый ресурс)
        /// </summary>
        private static int counter = 0;

        /// <summary>
        /// Объект синхронизации (структурного типа)
        /// </summary>
        /// <remarks>
        /// block - не должен быть структурным. 
        /// нужно использовать ссылочные типы 
        /// (которые будут находиться в куче, а не в стеке)
        /// </remarks>
        private static int block = 0;
        
        // для сравнения поведения lock (последний скрин)
        // private static object block = new object();

        private static void GetObjectAddress(object obj)
        {
            GCHandle gch = GCHandle.Alloc(obj, GCHandleType.Pinned);
            IntPtr pObj = gch.AddrOfPinnedObject();

            Console.WriteLine(pObj.ToString());
        }

        private static void MonitorFunction() 
        {
            for (int i = 0; i < 50; i++) 
            {
                // При боксинге создает новый объект в куче каждый раз
                // т.е. будет создано 50 новых объктов и 50 разных ссылок на объект
                var blockObj = (object)block;
                GetObjectAddress(blockObj);
                Monitor.Enter(blockObj);

                // работа
                Console.WriteLine(++counter);

                // тут опять создается новый объект в куче
                // и с него пытаются снять блокировку
                // но на него не была установлена блокировка
                blockObj = (object)block;
                GetObjectAddress(blockObj);
                Monitor.Exit(blockObj); // тут будет исключение так как пытаемся снять блокировку там где ее нет
            }
        }

        private static void LockFunction()
        {
            for (int i = 0; i < 50; i++)
            {
                lock((object)block)
                {
                    Console.WriteLine(++counter);
                }
            }
        }

        static void Main(string[] args)
        {
            //MonitorFunction();

            // ------------------------------------------

            Thread[] threads = new Thread[3];
            for (int i = 0; i < 3; i++)
            {
                threads[i] = new Thread(LockFunction);
                threads[i].Start();
            }

            Console.ReadKey();
        }
    }
}
