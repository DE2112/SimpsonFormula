using System;
using System.Linq;
using static System.Console;
using Eval = Z.Expressions.Eval;

namespace Num5
{
    class Program
    {
        private static string function;

        static void Main(string[] args)
        {
            WriteLine("Введите функцию (от одной переменной): ");
            function = ReadLine();

            WriteLine("Введите количество разбиений:");
            int n;
            while (!int.TryParse(ReadLine(), out n))
            {
                WriteLine("Неправильно введены даные");
            }

            var limits = Array.ConvertAll(ReadLine()?.Split(' '), x => float.Parse(x)).ToList();
            limits.Sort();

            var result = Simpson(limits[0], limits[1], n);

            if (!float.IsInfinity(result))
            {
                WriteLine(
                    $"Определенный интеграл функции f(x) = {function} на интервале ({limits[0]}, {limits[1]}) равен {result}");
            }
            else
            {
                WriteLine("Функция неограничена на заданом интервале");
            }
        }

        static float f(float x0)
        {
            if (x0 == 0f)
            {
                Console.Write("");
            }
            
            var y = Eval.Execute<float>(function, new {x = x0});
            return y;
        }
        
        static float Simpson(float a, float b, int n) {
            var width = (b-a)/n;
            var simpson_integral = 0f;

            for (int step = 0; step < n; step++) {
                var x1 = a + step * width;
                var x2 = a + (step + 1) * width;

                simpson_integral += (x2 - x1) / 6f * (f(x1) + 4f * f(0.5f * (x1 + x2)) + f(x2));
            }
            
            return simpson_integral;
        }
    }
}
