using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangleParams
{
    class Program
    {
        public class WrongNumberOfParametrsException : Exception
        {
            /// <summary>
            /// Введенное количество параметров
            /// </summary>
            public int Count { get; set; }
            public WrongNumberOfParametrsException(string message, int count)
                : base(message)
            {
                this.Count = count;
            }
        }
        public class WrongNumberOfArgumentsInFileException : Exception
        {
            /// <summary>
            /// Количетсво параметров в строке
            /// </summary>
            int Count { get; set; }
            string Filename { get; set; }
            int Row { get; set; }
            public WrongNumberOfArgumentsInFileException(string message, int count, string filename, int row)
                : base(message)
            {
                Count = count;
                Row = row;
                Filename = filename;
            }
        }
        public class ConvertArgumentException : ArgumentException
        {
            int Row { get; set; }
            public ConvertArgumentException(string message, string paramName, int row)
                : base(message, paramName)
            {
                Row = row;
            }
        }
        public static void ReadFile(string inFile, string outFile)
        {
            if (!File.Exists(inFile))
                throw new FileNotFoundException(string.Format("Файл {0} не существует", inFile), inFile);

            StreamReader sr = new StreamReader(inFile);
            StreamWriter sw = File.CreateText(outFile);
            try
            {
                int row = 0;
                while (!sr.EndOfStream)
                {
                    row++;
                    string[] mas = sr.ReadLine().Split(new char[] { ' ', '\t', ';' }, StringSplitOptions.RemoveEmptyEntries);

                    if (mas.Length != 3)
                        throw new WrongNumberOfArgumentsInFileException(string.Format("Неверное количество параметров"), mas.Length, inFile, row);

                    double side1, side2, angle1;
                    if (!double.TryParse(mas[0], out side1))
                        throw new ConvertArgumentException(string.Format("Ошибка при попытке преобразования параметра в дробное число. Строка {0}, параметр {1}", row, 1), "side1", row);
                    if (!double.TryParse(mas[1], out side2))
                        throw new ConvertArgumentException(string.Format("Ошибка при попытке преобразования параметра в дробное число. Строка {0}, параметр {1}", row, 2), "side2", row);
                    if (!double.TryParse(mas[2], out angle1))
                        throw new ConvertArgumentException(string.Format("Ошибка при попытке преобразования параметра в дробное число. Строка {0}, параметр {1}", row, 3), "angle1", row);

                    double side3 = GetSide(side1, side2, angle1);
                    string sides = string.Format("{0};{1};{2}", side1, side2, side3);
                    sw.WriteLine(sides);

                    double angle2 = GetAngle(side1, side2, angle1);
                    double angle3 = GetAngle(side1, side3, angle1);
                    Console.WriteLine(string.Format("Row {0}:   Angle 2={1};    Angle 3={2}", row, angle2, angle3));
                }
            }
            finally
            {
                sw.Close();
                sr.Close();
            }
        }
        /// <summary>
        /// Возвращает длину третьей стороны треугольника
        /// </summary>
        /// <param name="side1">Длина первой стороны</param>
        /// <param name="side2">Длина второй стороны</param>
        /// <param name="angle1">Угол между первой и второй стороной</param>
        /// <returns>Возвращает длину третьей стороны треугольника</returns>
        public static double GetSide(double side1, double side2, double angle1)
        {
            return Math.Sqrt(side1 * side1 + side2 * side2 - 2 * Math.Cos(DegreeToRad(angle1)) * side1 * side2);
        }
        /// <summary>
        /// Возвращает угол противолижащий строне side2
        /// </summary>
        /// <param name="side1">Первая сторона треугольника</param>
        /// <param name="side2">Вторая сторона треугольника</param>
        /// <param name="angle1">Угол, противолижащий стороне 1</param>
        /// <returns></returns>
        public static double GetAngle(double side1, double side2, double angle1)
        {
            return RadToDegree(Math.Asin(Math.Sin(DegreeToRad(angle1)) * side1 / side2));
        }
        /// <summary>
        /// Переводит радианы в градусы
        /// </summary>
        /// <param name="rad">Значение в радианах</param>
        /// <returns></returns>
        public static double RadToDegree(double rad)
        {
            return rad * 180 / Math.PI;
        }
        /// <summary>
        /// Переводит градусы в радианы
        /// </summary>
        /// <param name="degree">Значение в градусах</param>
        /// <returns></returns>
        public static double DegreeToRad(double degree)
        {
            return degree * Math.PI / 180;
        }
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 2)
                    throw new WrongNumberOfParametrsException("Неверное количество параметров. Необходимо ввести имена входного и выходного файлов", args.Length);

                ReadFile(args[0], args[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
