using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DichotomyMethod
{
    internal class Program
    {
        //Для примеров можно ввести начальный отрезок -10, конечный 10, шаг 1, точность 0,001
        static double func(double element)// задается функция
        {
            //return Math.Pow(element,5) + 2 * element + 5;
            return Math.Cos(2 * element) - 3 * Math.Pow(element, 2) + 1;
            //return Math.Pow(element,3) + element - 1;
        }
        static double funcp(double element)// задается произодная функции
        {
            //return 5 * Math.Pow(element, 4) + 2;
            return -2 * Math.Sin(2 * element) - 6 * element;
            //return 3 * Math.Pow(element,2) + 1;
        }
        static int GetDecimalDigitsCount(double number)// высчитывается количество знаков после запятой
        {
            string[] str = number.ToString(new System.Globalization.NumberFormatInfo() { NumberDecimalSeparator = "." }).Split('.');
            return str.Length == 2 ? str[1].Length : 0;
        }
        static void Main(string[] args)
        {

            Console.WriteLine("Введите начало отрезка: ");
            double start = Convert.ToDouble(Console.ReadLine());// создание начала отрезка
            Console.WriteLine("Введите конец отрезка: ");
            double end = Convert.ToDouble(Console.ReadLine());// создание конца отрезка
            Console.WriteLine("Введите шаг: ");
            double step = Convert.ToDouble(Console.ReadLine());// создание шага
            Console.WriteLine("Введите точность(знаков после запятой): ");
            double accuracy = Convert.ToDouble(Console.ReadLine());// создание точности ответа
            int count = GetDecimalDigitsCount(accuracy); // вычисление при помощи метода количетсва знаков после запятой


            double a = (end - start) / step + 1;
            double element = start;
            double step2 = step * 0.1;
            double[] mas = new double[Convert.ToInt32(a)];
            double[] mas_f = new double[Convert.ToInt32(a)];
            double startc = 0;
            double element1 = 0;
            double endc = 0;
            double b = (endc - startc) / step2 + 1;
            double[] mas_fp = new double[Convert.ToInt32(b)];
            double[] mas_fpf = new double[Convert.ToInt32(b)];
            int c = 0;
            double middle = 0;
            double fc = 0;
            double length = 0;
            double fa = 0;
            double fb = 0;


            for (int i = 0; i < a; i++, element += step)// внешний цикл, отвечающий за формирование массива x и f(x) 
            {
                mas[i] = element;// расчет х
                mas_f[i] = func(element);// расчет функции от х
                try
                {
                    if ((mas_f[i] <= 0 && mas_f[i - 1] >= 0) || (mas_f[i] >= 0 && mas_f[i - 1] <= 0))// проверка на подозрительный отрезок
                    {
                         startc = mas[i - 1];
                         endc = mas[i];
                         b = (endc - startc) / step2 + 1;
                         mas_fp = new double[Convert.ToInt32(b)];
                         mas_fpf = new double[Convert.ToInt32(b)];
                        Console.WriteLine($"Подозрительный отрезок:[{startc};{endc}]");// вывод в консоль подозрительного отрезка
                        element1 = startc;
                        for (int j = 0; j < b; j++, element1 += step2)// внутрений цикл, отвечающий за заполнение массива подозрительного отрезка
                        {
                            c = 0;
                            mas_fp[j] = element1;
                            mas_fpf[j] = funcp(element1);
                            Console.WriteLine($"{$"{j}",4} | {$"{mas_fp[j]}",4} | {$"{mas_fpf[j]}",7} ");
                            try
                            {
                                if ((mas_fpf[j] <= 0 && mas_fpf[j - 1] <= 0) || (mas_fpf[j] >= 0 && mas_fpf[j - 1] >= 0))// проверка на изменяемость знака значений на подозрительном отрезке
                                {
                                    c = 2;
                                }
                                else
                                {
                                    c = 1;
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                        if (c == 1)
                        {
                            Console.WriteLine("Отрезок не подходит, так как знак не сохраняется");
                        }
                        else if (c == 2)
                        {
                            Console.WriteLine("Отрезок подходит, так как знак сохраняется");
                            length = endc - startc;
                            while (length > 2 * accuracy)// цикл, выполняющийся пока уточненное решение не станет меньше удвоенного произведения точности
                            {
                                middle = (startc + endc) / 2;
                                fc = func(middle);
                                length = endc - startc;
                                fa = func(startc);
                                fb = func(endc);
                                if ((fc <= 0 && fa >= 0) || (fc >= 0 && fa <= 0))// условие для подстановки с вместо b
                                {
                                    endc = middle;

                                }
                                else if ((fc <= 0 && fb >= 0) || (fc >= 0 && fb <= 0))// условие для подстановки с вместо а
                                {

                                    startc = middle;
                                }
                            }
                            Console.WriteLine("Ответ" + Math.Round(middle,count));// вывод ответа в консоль
                        }

                    }
                }
                catch (Exception)
                {
                }
            }
            Console.ReadKey();
        }
    }
}
