<<<<<<< HEAD
﻿using System;
using static pycs.pycs;

namespace pycs.modules
{
    public static class math
    {
        public static double fabs(double x) => Math.Abs(x);
        public static double sqrt(double x) => Math.Sqrt(x);

        //angles
        public static double sin(double x) => Math.Sin(x);
        public static double cos(double x) => Math.Cos(x);
        public static double tan(double x) => Math.Tan(x);
        public static double sinh(double x) => Math.Sinh(x);
        public static double cosh(double x) => Math.Cosh(x);
        public static double tanh(double x) => Math.Tanh(x);

        public static double asin(double x) => Math.Asin(x);
        public static double acos(double x) => Math.Acos(x);
        public static double atan(double x) => Math.Atan(x);
        public static double asinh(double x) => Math.Asinh(x);
        public static double acosh(double x) => Math.Acosh(x);
        public static double atanh(double x) => Math.Atanh(x);
        public static double copysign(double x, double y) => Math.CopySign(x, y);

        public static bool isnan<T>(T x)
        {
            if (x is int || x is double || x is float || x is decimal)
                return false;
            else
                return true;
        }
        public static double trunc(double x) => Math.Truncate(x);
        
        public static double ceil(double x) => Math.Ceiling(x);
        public static double floor(double x) => Math.Floor(x);
        
        public static double log(double x) => Math.Log(x);
        public static double log10(double x) => Math.Log10(x);
        public static int factorial(int x)
        { 
            int i, fact;
            fact = x;
            for (i = x - 1; i >= 1; i--)
            {
                fact = fact * i;
            }
            return fact;
        }

        public static double exp(double x) => Math.Exp(x);

        //constants 
        public const double pi = 3.141592653589793;
        public const double tau = 6.283185307179586;
        public const double e = 2.718281828459045;
    }
}
=======
﻿using System;
using static pycs.pycs;

namespace pycs.modules
{
    public static class math
    {
        public static double fabs(double x) => Math.Abs(x);
        public static double sqrt(double x) => Math.Sqrt(x);

        //angles
        public static double sin(double x) => Math.Sin(x);
        public static double cos(double x) => Math.Cos(x);
        public static double tan(double x) => Math.Tan(x);
        public static double sinh(double x) => Math.Sinh(x);
        public static double cosh(double x) => Math.Cosh(x);
        public static double tanh(double x) => Math.Tanh(x);

        public static double asin(double x) => Math.Asin(x);
        public static double acos(double x) => Math.Acos(x);
        public static double atan(double x) => Math.Atan(x);
        public static double asinh(double x) => Math.Asinh(x);
        public static double acosh(double x) => Math.Acosh(x);
        public static double atanh(double x) => Math.Atanh(x);
        public static double copysign(double x, double y) => Math.CopySign(x, y);

        public static bool isnan<T>(T x)
        {
            if (x is int || x is double || x is float || x is decimal)
                return false;
            else
                return true;
        }
        public static double trunc(double x) => Math.Truncate(x);
        
        public static double ceil(double x) => Math.Ceiling(x);
        public static double floor(double x) => Math.Floor(x);
        
        public static double log(double x) => Math.Log(x);
        public static double log10(double x) => Math.Log10(x);
        public static int factorial(int x)
        { 
            int i, fact;
            fact = x;
            for (i = x - 1; i >= 1; i--)
            {
                fact = fact * i;
            }
            return fact;
        }

        public static double exp(double x) => Math.Exp(x);

        //constants 
        public const double pi = 3.141592653589793;
        public const double tau = 6.283185307179586;
        public const double e = 2.718281828459045;
    }
}
>>>>>>> origin/master
