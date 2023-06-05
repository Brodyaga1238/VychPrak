class Program
{ 
    static double Function(double x)
    {
        return x-10*Math.Sin(x);
    }   
    static void Main(string[] args)
    {
        double a = -3; // начальная точка отрезка
        double b = 5; // конечная точка отрезка
        double epsilon = 1e-6; // точность
        int n = 6; // количество точек на отрезке
        double h = Math.Abs((a - b) / (n)); // расчет шага для табулирования функции
        Console.WriteLine("ЧИСЛЕННЫЕ МЕТОДЫ РЕШЕНИЯ НЕЛИНЕЙНЫХ УРАВНЕНИЙ");
        Console.WriteLine($"Исходные параметры: A = {a}, B = {b}, epsilon = {epsilon}");
        Console.WriteLine("Вид функции: f(x) = x - 10 * sin(x)");
        DivideRoots(a,b,epsilon,n,h);
    }
    static void DivideRoots(double a, double b, double epsilon, int n,double h)
    {
        int Counter = 0;
        double x1 = a, x2 = x1 + h;
        double y1 = Function(x1);
        double y2;
        print();
        while (x2<=b)
        {
            y2 = Function(x2);
            if (y1 * y2 <= 0)
            {
                Counter++;
                Console.WriteLine($"На этом отрезке найден корень: [{x1}:{x2}]");
                print();
                bisection(x1,x2,epsilon);
                print();
                NewtonMethod(x1,x2,n, epsilon);
                print();
                ModNewtonMethod(x1,x2,epsilon);
                print();
                SecantMethod(x1,x2,epsilon,n);
                print();
            }
            x1 = x2;
            x2 = x1 + h;
            y1 = y2;
        }
        Console.WriteLine($"Количество отрезков с корнем: {Counter}");
    }

    static void print()
    {
        for (int i = 1; i < 3; i++)
        {
            Console.Write("---------------------------------------");
        }
        Console.WriteLine();
    }
    static void bisection(double A, double B, double epsilon)
    {
        double a = A;
        double b = B;
        int counter = 0;
        double c;
        if (Function(a) * Function(b) > 0)
        {
            Console.WriteLine($"На отрезке [{a}:{b}] корней нет");
            return;
        }
        while (Math.Abs(b - a) > epsilon) // Изменен критерий остановки: теперь нужно проверять длину отрезка, а не только значение функции в середине
        {
            c = (a + b) / 2;
            if (Function(a) * Function(c) < 0) b = c;
            else a = c;
            counter++;
        }
        double x = (a + b) / 2;
        double inaccuracy = Math.Abs(b - a) / 2; // Исправлено вычисление длины отрезка

        Console.WriteLine($"Метод бисекции:");
        Console.WriteLine($"Начальное приближение: [{A}:{B}]");
        Console.WriteLine($"Количество шагов: {counter}");
        Console.WriteLine($"Приближенное решение: x = {x}");
        Console.WriteLine($"Длина получившегося отрезка: {inaccuracy}");
        Console.WriteLine($"Абсолютная величина невязки решения: {Math.Abs(Function(x))}");
    }
    static double equat(double x)
    {
        return 1 - 10 * Math.Cos(x);
    }

    static void NewtonMethod(double a, double b, int n, double epsilon)
    {
        double x0 = (a + b) / 2;
        double x = 0; // Избыточное присваивание начального значения
        double deltax = Math.Abs(x0 - x);
        int counter = 0;
        for (int i = 1; i <= n; i++)
        {
            x = x0;
            double derivative = equat(x0);
            if (Math.Abs(derivative) < epsilon) // Проверка на разрыв производной
            {
                Console.WriteLine($"Метод Ньютона не сходится: разрыв производной на интервале [{a}:{b}]");
                return;
            }
            x0 = x0 - Function(x0) / derivative;
            deltax = Math.Abs(x0 - x);
            counter = i;
            if (Math.Abs(Function(x0)) < epsilon)
            {
                Console.WriteLine("Метод Ньютона:");
                Console.WriteLine($"Начальное приближение: {(a + b) / 2}");
                Console.WriteLine($"Количество шагов: {counter}");
                Console.WriteLine($"Приближенное решение: x = {x0}");
                Console.WriteLine($"| x_m - x_(m-1) | = {deltax}");
                Console.WriteLine($"Абсолютная величина невязки решения: {Math.Abs(Function(x0))}");
                return;
            }
        }
        Console.WriteLine($"Метод Ньютона:");
        Console.WriteLine($"Начальное приближение: {(a + b) / 2}");
        Console.WriteLine($"Количество шагов: {counter}");
        Console.WriteLine($"Приближенное решение: x = {x0}");
        Console.WriteLine($"| x_m - x_(m-1) | = {deltax}");
        Console.WriteLine($"Абсолютная величина невязки решения: {Math.Abs(Function(x0))}");
    }

    static void ModNewtonMethod(double a, double b, double epsilon)
    {
        double x0 = (a + b) / 2;
        double x = x0;
        double deltax = double.MaxValue; // Начальное значение deltax
        int counter = 0;

        while (Math.Abs(Function(x)) > epsilon && Math.Abs(deltax) > epsilon) // Условие выхода из цикла
        {
            double derivative = equat(x0); // значение производной в точке x0
            if (Math.Abs(derivative) < epsilon) // если производная близка к 0, метод не сходится
            {
                Console.WriteLine($"Метод модифицированного Ньютона не сходится: разрыв производной на интервале [{a}: {b}]");
                return;
            }
            counter++;
            x = x0 - Function(x) / derivative; // новое приближение
            deltax = Math.Abs(x - x0); // вычисляем изменение x
            x0 = x;
        }
        Console.WriteLine($"Метод модифицированного Ньютона:");
        Console.WriteLine($"Начальное приближение: {(a + b) / 2}");
        Console.WriteLine($"Количество шагов: {counter}");
        Console.WriteLine($"Приближенное решение: x = {x}");
        Console.WriteLine($"| x_m - x_(m-1) | = {Math.Abs(deltax)}");
        Console.WriteLine($"Абсолютная величина невязки: {Math.Abs(Function(x))}");
    }
    static void SecantMethod(double A, double B, double epsilon, int n)
    {
        double x0 = A;
        double x1 = B;
        int counter = 0;
        double x2 = 0, deltax = 0;
        double absoluteError = 0;

        Console.WriteLine("Метод секущих");
        while (counter < n)
        {
            counter++;
            double f0 = Function(x0); // значение функции в точке x0
            double f1 = Function(x1); // значение функции в точке x1
            x2 = x1 - f1 * (x1 - x0) / (f1 - f0); // новое приближение
            absoluteError = Math.Abs(Function(x2));

            if (Math.Abs(x2 - x1) < epsilon)
            {
                Console.WriteLine($"Начальное приближение: [{A}:{B}]");
                Console.WriteLine($"Количество шагов: {counter}");
                Console.WriteLine($"Приближенное решение: x = {x2}");
                Console.WriteLine($"| x_m - x_(m-1) | = {Math.Abs(deltax)}");
                Console.WriteLine($"Абсолютная величина невязки: {absoluteError}");
                return;
            }

            x0 = x1;
            x1 = x2;
            deltax = x1 - x0;
        }
        Console.WriteLine($"Начальное приближение: [{A}:{B}]");
        Console.WriteLine($"Количество шагов: {counter}");
        Console.WriteLine($"Приближенное решение: x = {x2}");
        Console.WriteLine($"| x_m - x_(m-1) | = {Math.Abs(deltax)}");
        Console.WriteLine($"Абсолютная величина невязки: {absoluteError}");
    }
}
