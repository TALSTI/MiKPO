using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;

namespace MiKPO_Lab_2
{
    class Program
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

        public struct ImagePart
        {
            public int[,] Colors;
            public int X, Y;
            public ImagePart(int[,] c, int x, int y)
            {
                this.Colors = c;
                this.X = x;
                this.Y = y;
            }
        }

        public static int[,] ColorsToInts(Bitmap bm)
        {
            int width = bm.Width, height = bm.Height;
            int[,] result = new int[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color c = bm.GetPixel(x, y);
                    result[x, y] = c.ToArgb();
                }
            }
            return result;
        }

        public static Bitmap IntsToColors(List<ImagePart> P)
        {
            Bitmap result = new Bitmap(before.Width, before.Height);
            foreach (ImagePart IP in P)
            {
                result.SetPixel(IP.X, IP.Y, Color.FromArgb(IP.Colors[NetSize / 2, NetSize / 2]));
            }
            return result;
        }

        public const byte NetSize = 3;

        public static List<ImagePart> PartsBefore = new List<ImagePart>();
        public static BlockingCollection<ImagePart> PartsAfter = new BlockingCollection<ImagePart>();

        public static void ApplyFilter(Bitmap bm)
        {
            int[,] pixels = ColorsToInts(bm);
            int width = pixels.GetLength(0), height = pixels.GetLength(1);
            for (int i = 0; i < width - (NetSize - 1); i++)
            {
                for (int j = 0; j < height - (NetSize - 1); j++)
                {
                    int[,] Part = new int[NetSize, NetSize];
                    for (int k = 0; k < NetSize; k++)
                    {
                        for (int l = 0; l < NetSize; l++)
                        {
                            Part[k, l] = pixels[i + k, j + l];
                        }
                    }
                    PartsBefore.Add(new ImagePart(Part, i, j));
                }
            }
            Parallel.ForEach(PartsBefore, p =>
                {
                    int[] buf = new int[NetSize * NetSize];
                    int Nh = NetSize / 2;
                    int index = 0;
                    for (int i = 0; i < NetSize; i++)
                    {
                        for (int j = 0; j < NetSize; j++)
                        {
                            buf[index] = p.Colors[i, j];
                            index++;
                        }
                    }
                    Array.Sort(buf);
                    p.Colors[Nh, Nh] = buf[NetSize * NetSize / 2];
                    PartsAfter.Add(new ImagePart(p.Colors, p.X, p.Y));
                });
        }

        public static Bitmap before, after;

        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 2)
                    throw new ParametersException("Неверное количество параметров. Необходимо ввести имена входного и выходного файлов", args.Length);
                if (!File.Exists(args[0]))
                    throw new FileNotFoundException(string.Format("Файл {0} не существует", args[0]), args[0]);
                before = new Bitmap(args[0]);
                ApplyFilter(before);
                after = IntsToColors(PartsAfter.ToList());
                after.Save(args[1], System.Drawing.Imaging.ImageFormat.Bmp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
