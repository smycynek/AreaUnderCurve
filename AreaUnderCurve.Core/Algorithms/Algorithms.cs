using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AreaUnderCurve.Core
{
    /// <summary>
    /// A few popular riemann sum algorithms
    /// </summary>
    public static partial class Algorithms
    {
        public static double Simpson(Polynomial polynomial, double lowerBound, double upperBound)
        {
            var lowerVal = polynomial.Evaluate(lowerBound);
            var upperVal = polynomial.Evaluate(upperBound);
            var midpointVal = polynomial.Evaluate((lowerBound + upperBound) / 2);
            return ((upperBound - lowerBound) / 6) * (lowerVal + (4 * midpointVal) + upperVal);
        }

        public static double Trapezoid(Polynomial polynomial, double lowerBound, double upperBound)
        {
            var lowerVal = polynomial.Evaluate(lowerBound);
            var upperVal = polynomial.Evaluate(upperBound);
            return (upperBound - lowerBound) *((lowerVal + upperVal) / 2);
        }

        public static double Midpoint(Polynomial polynomial, double lowerBound, double upperBound)
        {
            return (upperBound - lowerBound) * (polynomial.Evaluate((lowerBound + upperBound) / 2));
        }

        public static Func<Polynomial, double, double, double> GetAlgorithm(string name)
        {
            if (_functionMap.Count == 0)
                Init();

            if (ExtractRombergSuffix(name, out int n, out int m))
            {
                return RombergFactory.MakeRombergFunction(n, m);
            }
            else
            {
                if (!_functionMap.ContainsKey(name))
                    throw new ArgumentException($"Algorithm not found: {name}");
                return _functionMap[name];
            }
        }

        #region Implementation
        /// <summary>
        /// Initializes map of algorithm names to functions on-demand.
        /// </summary>
        private static void Init()
        {
            //It's possible to do this automatically with reflection, but the code is extremely verbose, and I
            //didn't think it was worth it for something this simple.  I could have made the algorithm a class,
            //but I wanted the user to be able to easily pass a lambda with their own algorithm as well.
            _functionMap.Add(nameof(Simpson), Simpson);
            _functionMap.Add(nameof(Trapezoid), Trapezoid);
            _functionMap.Add(nameof(Midpoint), Trapezoid);
            _functionMap.Add("Romberg", RombergFactory.MakeRombergFunction(4, 3));
        }

        private static bool ExtractRombergSuffix(string algorithmName, out int n, out int m)
        {
            m = 0;
            n = 0;
            
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("Romberg[0-9][0-9]");
             var match = regex.Match(algorithmName);
            if (!match.Success)
            {
                return false;
            }
            else
            {
                n = int.Parse(algorithmName[7].ToString());
                m = int.Parse(algorithmName[8].ToString());
                return true;
            }
            
        }

        public static IEnumerable<string> GetAlgorithms()
        {
            return _functionMap.Keys;
        }

        private static Dictionary<string, Func<Polynomial, double, double, double>> _functionMap = new Dictionary<string, Func<Polynomial, double, double, double>>();
        #endregion

    }
}

