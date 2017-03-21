using System;
using AreaUnderCurve.Core;

  
namespace AreaUnderCurve.Demo
{
    public class Program
    {
        //Simple demo
        public static void Main(string[] args)
        {
            Utility.Log("Try out some sample polynomials, bounds, step sizes, and algorithms.");
            var trapezoid = Algorithms.GetAlgorithm("Trapezoid");
            var midpoint = Algorithms.GetAlgorithm("Midpoint");
            var simpson = Algorithms.GetAlgorithm("Simpson");
            var boundsSimple1 = new Bounds(0, 10, .1);
            var boundsSimple2 = new Bounds(0, 10, 1);
            var boundsSymmetric1 = new Bounds(-5, 5, .1);
            var polynomialSimpleCubic = new Polynomial(new System.Collections.Generic.SortedDictionary<double, double> { [3] = 1 });
            var polynomialSimpleFraction = new Polynomial(new System.Collections.Generic.SortedDictionary<double, double> { [.5] = 1 });

            Utility.Log("-Demo 1");
            Utility.Log($"Area={AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomialSimpleCubic, boundsSimple1, midpoint)}");

            Utility.Log("\n-Demo 2 -- larger step size, lower accuracy");
            Utility.Log($"Area={AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomialSimpleCubic, boundsSimple2, midpoint)}");

            Utility.Log("\n-Demo 3 -- symmetric bounds and a symmetric function (net area close to zero)");
            Utility.Log($"Area={AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomialSimpleCubic, boundsSymmetric1, simpson)}");

            Utility.Log("\n-Demo 4 -- fractional exponents");
            // integral of f(x)=x^.5 is (x^1.5)1.5 + sc, or (10*sqrt(10))/1.5 with these bounds
            Utility.Log($"Area={AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomialSimpleFraction, boundsSimple1, trapezoid)}");


        }
    }
}