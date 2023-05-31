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
        Console.WriteLine($"Вид функции: f(x) = x - 10 * sin(x)");
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
                Console.WriteLine($"На этом отрезке найден корень: [{x1},{x2}]");
                print();
                bisection(x1,x2,epsilon);
                print();
                NewtonMethod(x1,x2,n, epsilon);
                print();
                ModNewtonMethod(x1,x2,epsilon);
                print();
                SecantMethod(x1,x2,epsilon);
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
        int counter=0;
        double c=(a + b) / 2;;
        if (Function(a) * Function(b) > 0)
        {
            Console.WriteLine($"На отрезке [{a},{b}] корней нет");
            return;
        }
        while(Math.Abs(Function(c) - 0) > epsilon)
        {
            if (Function(a) * Function(c) < 0) b = c;
            else a = c;
            counter++;
            c=(a + b) / 2;
        }
        if (counter == 0) counter = 1;
        double x = (a + b) / 2;
        double inaccuracy = (b - a) / 2;

        Console.WriteLine($"Метод бисекции");
        Console.WriteLine($"Начальное приближение: [{A},{B}]");
        Console.WriteLine($"количество шагов {counter}");
        Console.WriteLine($"Приближенное решение x = {x}");
        Console.WriteLine($"Длина получившегося отрезка = {inaccuracy}");
        Console.WriteLine($"Абсолютная величина невязки решения {Math.Abs(Function(x)-0)}");
    }
    static double equat(double x)
    {
        return 1 - 10 * Math.Cos(x);
    }
    
    static void NewtonMethod(double a, double b, int n, double epsilon)
    {
        double x0 = (a + b) / 2;
        double x = 0;
        double deltax = Math.Abs(x0 - x);
        int counter = 0;
        for (int i = 1; i <= n; i++)
        {
            x = x0;
            x0 = x0 - Function(x0) / equat(x0);
            deltax = Math.Abs(x0 - x);
            counter = i;
            if (Math.Abs(Function(x0)) < epsilon)
            {
                Console.WriteLine($"Метод Ньютона");
                Console.WriteLine($"Начальное приближение: {(a + b) / 2}");
                Console.WriteLine($"количество шагов {counter}");
                Console.WriteLine($"Приближенное решение x = {x}");
                Console.WriteLine($"|x_m-x_(m-1)| = {deltax}");
                Console.WriteLine($"Абсолютная величина невязки решения {Math.Abs(Function(x)-0)}");
                return;
            }
        }
        Console.WriteLine($"Метод Ньютона");
        Console.WriteLine($"Начальное приближение: {(a + b) / 2}");
        Console.WriteLine($"количество шагов {counter}");
        Console.WriteLine($"Приближенное решение x = {x}");
        Console.WriteLine($"|x_m-x_(m-1)| = {deltax}");
        Console.WriteLine($"Абсолютная величина невязки решения {Math.Abs(Function(x)-0)}");
    }

    static void ModNewtonMethod(double a, double b, double epsilon)
    {
        double x0 = (a + b) / 2;
        double x = x0;
        double deltax = double.MaxValue;
        int counter = 0;

        while (Math.Abs(Function(x)) > epsilon && Math.Abs(deltax) > epsilon)
        {
            if (Math.Abs(equat(x0)) < epsilon)
            {
                Console.WriteLine($"Метод модифицированного Ньютона не сходится: разрыв производной на интервале [{a}, {b}]");
                return;
            }
            counter++;
            x = x0 - Function(x) / equat(x0);
            deltax = x - x0;
            x0 = x;
        }
        Console.WriteLine($"Метод модифицированного Ньютона:");
        Console.WriteLine($"Начальное приближение: {(a + b) / 2}");
        Console.WriteLine($"Количество шагов: {counter}");
        Console.WriteLine($"Приближенное решение: x = {x}");
        Console.WriteLine($"| x_m - x_(m-1) | = {Math.Abs(deltax)}");
        Console.WriteLine($"Абсолютная величина невязки: {Math.Abs(Function(x))}");
    }
    static void SecantMethod(double A, double B, double epsilon)
    {
        double x0 = A;
        double x1 = B;
        int counter = 0;
        double x2 = 0, deltax=0;
        double absoluteError = 0;

        Console.WriteLine("Метод секущих");
        while (counter < 6)
        {
            counter++;
            x2 = x1 - (Function(x1) * (x1 - x0)) / (Function(x1) - Function(x0));
            absoluteError = Math.Abs(Function(x2));

            if (Math.Abs(x2 - x1) < epsilon)
            {
                Console.WriteLine($"Начальное приближение: [{A}{B}]");
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
        Console.WriteLine($"Начальное приближение: [{A}{B}]");
        Console.WriteLine($"Количество шагов: {counter}");
        Console.WriteLine($"Приближенное решение: x = {x2}");
        Console.WriteLine($"| x_m - x_(m-1) | = {Math.Abs(deltax)}");
        Console.WriteLine($"Абсолютная величина невязки: {absoluteError}");
    }
}
