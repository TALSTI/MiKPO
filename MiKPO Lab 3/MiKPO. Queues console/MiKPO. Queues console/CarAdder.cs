using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiKPO.Queues_console
{
    class CarAdder
    {
        public static void AddCars(Queue<Car>[] queues, int countCar, int countKass)
        {
            Random rnd = new Random();
            int carNumber = 0;
            double tNow = 0;
            int sleep = 1000;

            while (true)
            {
                for (int i = 0; i < countCar; i++)
                {
                    int KassNumber = rnd.Next(countKass);
                    Car car = new Car((carNumber++).ToString(), tNow / 1000);
                    queues[KassNumber].Enqueue(car);
                    tNow += sleep;
                    Console.WriteLine(string.Format("Добавили машину {0} в кассу {1}", car, KassNumber));
                }
                System.Threading.Thread.Sleep(sleep);
            }
        }
    }
}
