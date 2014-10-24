using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiKPO.Queues_console
{
    public class ParametersException : Exception
    {
        public int Count { get; set; }
        public ParametersException(string message, int count)
            : base(message)
        {
            this.Count = count;
        }
    }

    class Program
    {
        static Queue<Car>[] queues = null;
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 5)
                    throw new ParametersException("Неверное количество параметров!", args.Length);
                int countKass = int.Parse(args[0]);
                int countIn = int.Parse(args[1]);
                int tObrMin = int.Parse(args[2]);
                int tObrMax = int.Parse(args[3]);
                int tView = int.Parse(args[4]);

                queues = new Queue<Car>[countKass];
                System.Threading.Thread[] threads = new System.Threading.Thread[countKass];
                for (int i = 0; i < countKass; i++)
                {
                    queues[i] = new Queue<Car>();
                    // threads[i] = new System.Threading.Thread(() => ProcessCar(i, tObrMin, tObrMax));
                    ThreadWithState tws = new ThreadWithState(i, tObrMin, tObrMax);
                    threads[i] = new System.Threading.Thread(() => tws.Process(queues));
                    threads[i].IsBackground = false;
                }

                for (int i = 0; i < countKass; i++)
                    threads[i].Start();

                System.Threading.Thread adder = new System.Threading.Thread(() => CarAdder.AddCars(queues, countIn, countKass));
                adder.IsBackground = false;
                adder.Start();

                System.Threading.Thread.Sleep(tView * 1000);

                for (int i = 0; i < countKass; i++)
                    threads[i].Abort();
                adder.Abort();

                int ost = queues[0].Count;
                for (int i = 1; i < countKass; i++)
                    ost += queues[i].Count;

                Console.WriteLine("Осталось машин: {0}", ost);
                Console.WriteLine("Конец.");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
