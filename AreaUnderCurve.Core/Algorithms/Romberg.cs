using System;
using System.Collections.Generic;
using System.Text;

namespace AreaUnderCurve.Core
{


    public  class RombergFactory
    {
  
      
        public static Func<Polynomial, double, double, double> MakeRomberg(int n, int m)
        {
            if ((m > n) || (m <= 1))
                throw new ArgumentException("N must be ?= m  and m >=1");

            Func<Polynomial, double, double, double> rombergFunc = delegate (Polynomial polynomial, double lowerBound, double upperBound)
            {
                return Romberg(n,m,polynomial,lowerBound,upperBound);
            };
            return rombergFunc;
        }

        private static double Romberg(int n, int m, Polynomial polynomial, double lowerBound, double upperBound)
        {
            if ((n == 0) && (m == 0))
                return h(1,lowerBound,upperBound) * (polynomial.Evaluate(lowerBound) + polynomial.Evaluate(upperBound));

            else if (m == 0)
                return (.5 * Romberg(n - 1, 0,polynomial,lowerBound,upperBound)) + (h(n,lowerBound,upperBound) * sum(n,polynomial,lowerBound,upperBound));
            else
                return (1 / (Math.Pow(4, m) - 1))   * ((Math.Pow(4, m) * Romberg(n, m - 1, polynomial, lowerBound, upperBound)) - Romberg(n - 1, m - 1, polynomial, lowerBound, upperBound));

        }

        static double h(int n, double lowerBound, double upperBound)
        {
            return (1.0 / (Math.Pow(2, n))) * (upperBound - lowerBound);
        }


        static double sum(int n, Polynomial polynomial, double lowerBound, double upperBound)
        {
            double total = 0;
            for (int k = 1; k <= upperSumBound(n); k++)
            {
                total += sumfunc(k, n, polynomial, lowerBound, upperBound);
            }
            return total;
        }
        static double sumfunc(int k, int n, Polynomial p, double lowerBound, double upperBound)
        {
           return p.Evaluate(lowerBound + ((2 * k - 1) * h(n,lowerBound,upperBound)));
        }

        static int upperSumBound(int n)
        {
            return (int)Math.Pow(2, n - 1);
        }


        /*
        public double saveromberg(int n, int m, Polynomial p, double l, double u)
        {
            if ((n == 0) && (m == 0))
                return h(1) * (_p.Evaluate(_lowerBound) + _p.Evaluate(_upperBound));

            else if (m == 0)
                return (.5 * romberg(n - 1, 0)) + (h(n) * sum(n));
            else
                return (1 / (Math.Pow(4, m) - 1)) * ((Math.Pow(4, m) * romberg(n, m - 1)) - romberg(n - 1, m - 1));

        }
        */


    }
}
