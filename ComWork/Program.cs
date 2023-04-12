
using System;
using System.Net.Sockets;
using System.Security.Principal;
using System.Xml;
class Program
{   
    static double Function(double x)
    {
       // return x - 10 * Math.Sin(x);
       return x-10*Math.Sin(x);
    }   
    static void Main(string[] args)
    {
        double A = -3; // начальная точка отрезка
        double B = 5; // конечная точка отрезка
        double epsilon = 1e-8; // точность
        int N = 3; // количество точек на отрезке
        double h = Math.Abs((B - A) / (N)); // расчет шага для табулирования функции

        Console.WriteLine("ЧИСЛЕННЫЕ МЕТОДЫ РЕШЕНИЯ НЕЛИНЕЙНЫХ УРАВНЕНИЙ");
        Console.WriteLine($"Исходные параметры: A = {A}, B = {B}, epsilon = {epsilon}");
        Console.WriteLine($"Вид функции: f(x) = x - 10 * sin(x)\n");
        DivideRoots(A,B,epsilon,h);
        bisection(A,B,epsilon);
    } 
    /* 
    bisection
    x = 4.461978406906134
    N iterations = 22
    |x_m-x_m-1| = 9.53674295089968e-09
    |f(x_m)| = 2.7217208042884522e-08
    */
    static void DivideRoots(double a, double b, double epsilon, double h)
    {
        int Counter = 0;
        double x1 = a, x2 = x1 + h;
        double y1 = Function(x1);
        double y2;
        while (x2<=b)
        {
            y2 = Function(x2);
            if ( Function(a) * Function(b)<= 0)
            {
                Counter++;
                Console.WriteLine($"[{x1},{x2}]");
            }
            x1 = x2;
            x2 = x1 + h;
            y1 = y2;
        }
        Console.WriteLine($"{Counter}");
    }
    static void bisection(double a, double b, double eps)
    {
        int counter=0;
        double c=(a + b) / 2.00;
        while(b-a>2*eps)
        {
            c=(a + b) / 2.00;
            if (Function(a) * Function(c) <= 0) b = c;
            else a = c;
            counter++;
        }
        double x = (a + b) / 2.00;
        double inaccuracy = (b - a) / 2.00;
        Console.WriteLine($"Метод бисекции завершен\t x = {x} , Длина получившегося отрезка = {inaccuracy} , количество шагов {counter}, Абсолютная величина невязки решения {Math.Abs(Function(x)-0)}");
    }

    static double equat(double x)
    {
        return 1 - 10 * Math.Cos(x);
    }
    
    static void newtonMethod(double a, double b, double eps)
    {
        double x0 = (a + b) / 2;
        int m2 = 1;
        double df = 1 - 10 * Math.Cos(x0);
        double fx0 = x0 - 10 * Math.Sin(x0);
        Console.WriteLine("метод ньютона");
        while (Math.Abs(fx0 / df) >= eps)
        {
            Console.WriteLine($"Шаг {m2}: x{m2 - 1} = {x0} f(x{m2 - 1}) = {fx0} |x{m2} - x{m2 - 1}| = {Math.Abs(x0 - fx0/df)}");
            x0 = x0 - fx0 / df;
            fx0 = x0 - 10 * Math.Sin(x0);
            df = 1 - 10 * Math.Cos(x0);
            m2++;
        }
        Console.WriteLine($"Найден корень уравнения: x = {x0}");
    }
}

         

         
         


