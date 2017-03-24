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
            var romberg43 = Algorithms.GetAlgorithm("Romberg43");
            var boundsSimple1 = new Bounds(0, 10, .1);
            var boundsSimple2 = new Bounds(0, 10, 1);
            var boundsSimple3 = new Bounds(0, 10, 10);
            var boundsSymmetric1 = new Bounds(-5, 5, .1);
            var polynomialSimpleCubic = new Polynomial(new System.Collections.Generic.SortedDictionary<double, double> { [3] = 1 });
            var polynomialSimpleQuartic = new Polynomial(new System.Collections.Generic.SortedDictionary<double, double> { [4] = 1 });
            var polynomialSimpleFraction = new Polynomial(new System.Collections.Generic.SortedDictionary<double, double> { [.5] = 1 });

            Utility.Log("-Demo, Romberg vs MidPoint");
            Utility.Log($"{polynomialSimpleCubic.ToString()}, {boundsSimple1.ToString()}, MidPoint");
            Utility.Log($"Area={AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomialSimpleCubic, boundsSimple1, midpoint)}");

            Utility.Log("\n----");
            Utility.Log($"{polynomialSimpleCubic.ToString()}, {boundsSimple1.ToString()}, Romberg43");
            Utility.Log($"Area={AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomialSimpleCubic, boundsSimple1, romberg43)}");
       
            Utility.Log("\n-Demo -- larger step size, lower accuracy");
            Utility.Log($"{polynomialSimpleCubic.ToString()}, {boundsSimple2.ToString()}, MidPoint");
            Utility.Log($"Area={AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomialSimpleCubic, boundsSimple2, midpoint)}");

            Utility.Log("\n----");
            Utility.Log($"{polynomialSimpleCubic.ToString()}, {boundsSimple2.ToString()}, Trapezoid");
            Utility.Log($"Area={AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomialSimpleCubic, boundsSimple2, romberg43)}");

            Utility.Log("\n----");
            Utility.Log($"{polynomialSimpleCubic.ToString()}, {boundsSimple2.ToString()}, Simpson");
            Utility.Log($"Area={AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomialSimpleCubic, boundsSimple2, romberg43)}");

            Utility.Log("\n----");
            Utility.Log($"{polynomialSimpleCubic.ToString()}, {boundsSimple2.ToString()}, Romberg43");
            Utility.Log($"Area={AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomialSimpleCubic, boundsSimple2, romberg43)}");

            Utility.Log("\n-Demo -- Romberg43 with stepSize of 10 (no initial subdivisions, since Romberg subdivides on its own anyway");
            Utility.Log($"{polynomialSimpleQuartic.ToString()}, {boundsSimple3.ToString()}, Romberg43");
            Utility.Log($"Area={AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomialSimpleQuartic, boundsSimple3, romberg43)}");

            Utility.Log("\n-Demo -- Romberg21 with stepSize of 10 (no initial subdivisions, since Romberg subdivides on its own anyway");
            var romberg21 = RombergFactory.MakeRombergFunction(2, 1);
            Utility.Log($"{polynomialSimpleQuartic.ToString()}, {boundsSimple3.ToString()}, romberg21");
            Utility.Log($"Area={AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomialSimpleQuartic, boundsSimple3, romberg21)}");

            Utility.Log("\n-Demo -- Romberg11 with stepSize of 10 (no initial subdivisions, since Romberg subdivides on its own anyway");
            var romberg11 = RombergFactory.MakeRombergFunction(1, 1);
            Utility.Log($"{polynomialSimpleQuartic.ToString()}, {boundsSimple3.ToString()}, romberg11");
            Utility.Log($"Area={AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomialSimpleQuartic, boundsSimple3, romberg11)}");


            Utility.Log("\n-Demo -- symmetric bounds and a symmetric function (net area close to zero)");
            Utility.Log($"{polynomialSimpleCubic.ToString()}, {boundsSymmetric1.ToString()}, Simpson");
            Utility.Log($"Area={AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomialSimpleCubic, boundsSymmetric1, simpson)}");

            Utility.Log("\n-Demo -- fractional exponents");
            Utility.Log($"{polynomialSimpleFraction.ToString()}, {boundsSimple1.ToString()}, Trapezoid");
            // integral of f(x)=x^.5 is (x^1.5)1.5 + sc, or (10*sqrt(10))/1.5 with these bounds
            Utility.Log($"Area={AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomialSimpleFraction, boundsSimple1, trapezoid)}");


        }

        

        
    }
}