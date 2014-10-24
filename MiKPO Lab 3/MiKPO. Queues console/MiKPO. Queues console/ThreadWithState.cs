using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiKPO.Queues_console
{
    class ThreadWithState
    {
        int KassNum;
        int tObrMin;
        int tObrMax;
        public void Process(Queue<Car>[] queues)
        {
            Random rnd = new Random();
            Console.WriteLine("Пришло " + KassNum);
            while (true)
            {
                while (queues[KassNum].Count == 0)
                    System.Threading.Thread.Sleep(0);

                Car car = queues[KassNum].Dequeue();
                int tOb = rnd.Next(tObrMin, tObrMax + 1);
                car.tOut = car.tIn + tOb;

                Console.WriteLine(string.Format("Обработали машину {0}. Время входа {1}, Время выхода {2}", car, car.tIn, car.tOut));

                System.Threading.Thread.Sleep(tOb * 1000);
            }
        }
        public ThreadWithState(int kass, int tmin, int tmax)
        {
            KassNum = kass;
            tObrMin = tmin;
            tObrMax = tmax;
        }
    }
}
