using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace AreaUnderCurve.Core
{
    public static class Algorithms
    {
        private static Dictionary<string, Func<Polynomial, double, double, double>> _functionMap = new Dictionary<string, Func<Polynomial, double, double, double>>();
        public static void Init()
        {
            _functionMap.Add(nameof(Simpson), Simpson);
            _functionMap.Add(nameof(Trapezoid), Trapezoid);
            _functionMap.Add(nameof(Midpoint), Trapezoid);
        }

        public static  double Simpson(Polynomial polynomial, double lowerBound, double upperBound)
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
            if (!_functionMap.ContainsKey(name))
                throw new ArgumentException($"Algorithm not found: {name}");
            return _functionMap[name];
        }
    }
}
