using System;
using System.Collections.Generic;

namespace AreaUnderCurve.Core
{
    public class Polynomial
    {
        public Polynomial(SortedDictionary<double, double> coefficients)
        {
            FractionalExponents = false;
            _coefficients = coefficients;
            foreach (double e in _coefficients.Keys)
            {
                if (e < 0)
                    throw new ArithmeticException($"Negative exponents not supported: {e}");
                if ((e % 1) != 0)
                    FractionalExponents = true;

            }
        }
        private SortedDictionary<double, double> _coefficients;
        public override string ToString()
        {
            List<string> terms = new List<string>();
            foreach (double e in _coefficients.Keys)
            {
                terms.Add(FormatTerm(e, _coefficients[e]));

            }
            terms.Reverse();
            
            return $"f(x)={string.Join(" + ", terms)}";
        }

        private string FormatTerm(double exponent, double coefficient)
        {
            string exp;
            if (exponent == 0)
                exp = string.Empty;
            else if (exponent == 1)
                exp = "x";
            else exp = $"x^{exponent}";

            if (coefficient == 0)
            {
                if (exponent == 0)
                    return "0";
                else
                    return string.Empty;
            }

            if (coefficient == 1)
                return exp;

            else return $"{coefficient}{exp}";
        }
        public double Evaluate(double value)
        {
            if ((value < 0) && FractionalExponents)
                throw new ArithmeticException($"Fractional exponents not supported for negative inputs {value}");
            double total = 0;
            foreach (double e in _coefficients.Keys)
            {
                double coefficient = _coefficients[e];
                total += (coefficient * (Math.Pow(value, e)));

            }
            return total;
        }
        public bool FractionalExponents { get; private set; }
    }
}
