using System;
using System.Collections.Generic;
using System.Text;

namespace AreaUnderCurve.Core
{

    /// <summary>
    ///This is a very naive implementation of Romberg's method for numerical integration
    ///that I pieced together from
    ///https://en.wikipedia.org/wiki/Romberg's_method
    ///
    ///It is hardly optimized (or well factored) :), and I will work on something that uses iteration and memoization if I can.
    /// </summary>
    public class RombergFactory
    {
  
      
        /// <summary>
        /// Uses function currying to create an n-m Romberg approximation function.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Func<Polynomial, double, double, double> MakeRombergFunction(int n, int m)
        {
            if ((m > n) || (m < 1))
                throw new ArgumentException("N must be >= m  and m >=1");

            Func<Polynomial, double, double, double> rombergFunction = delegate (Polynomial polynomial, double lowerBound, double upperBound)
            {
                return Romberg(n ,m ,polynomial, lowerBound, upperBound);
            };
            return rombergFunction;
        }

        /// <summary>
        /// Romberg implementation
        /// </summary>
        private static double Romberg(int n, int m, Polynomial polynomial, double lowerBound, double upperBound)
        {
            if ((n == 0) && (m == 0))
            {
                return H(1, lowerBound, upperBound) *
                        (polynomial.Evaluate(lowerBound) + polynomial.Evaluate(upperBound));
            }
            else if (m == 0)
            {
                return (.5 * Romberg(n - 1, 0, polynomial, lowerBound, upperBound)) + 
                    (H(n, lowerBound, upperBound) * Sum(n, polynomial, lowerBound, upperBound));
            }

            else
            {
                return (1 / (Math.Pow(4, m) - 1)) *
                    (
                        (Math.Pow(4, m) * Romberg(n, m - 1, polynomial, lowerBound, upperBound))
                        - Romberg(n - 1, m - 1, polynomial, lowerBound, upperBound)
                    );
            }

        }

        //Helper functions
        private static double H(int n, double lowerBound, double upperBound)
        {
            return (1.0 / (Math.Pow(2, n))) * (upperBound - lowerBound);
        }

        private static double Sum(int n, Polynomial polynomial, double lowerBound, double upperBound)
        {
            double total = 0;
            var upperSumBound = UpperBoundOfSum(n);
            for (int k = 1; k <= upperSumBound; k++)
            {
                total += SumFunction(k, n, polynomial, lowerBound, upperBound);
            }
            return total;
        }
        private static double SumFunction(int k, int n, Polynomial polynomial, double lowerBound, double upperBound)
        {
           return polynomial.Evaluate(lowerBound + ((2 * k - 1) * H(n,lowerBound, upperBound)));
        }

        private static int UpperBoundOfSum(int n)
        {
            return (int)Math.Pow(2, n - 1);
        }

    }
}
