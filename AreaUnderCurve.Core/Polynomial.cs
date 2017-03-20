using System;
using System.Collections.Generic;

namespace AreaUnderCurve.Core
{

    /// <summary>
    /// A class to represent, evaluate, and display a polynomial
    /// </summary>
    public class Polynomial
    {

        /// <summary>
        /// Create a polynomial from a map of exponents to coefficents.
        /// e.g. {3:2, 0:-1.5} = 2x^3 - 1.5
        /// </summary>
        /// <param name="exponentToCoefficientMap"></param>
        public Polynomial(SortedDictionary<double, double> exponentToCoefficientMap)
        {
            FractionalExponents = false;
            _exponentToCoefficientMap = exponentToCoefficientMap;
            foreach (double exponent in _exponentToCoefficientMap.Keys)
            {
                if (exponent < 0)
                    throw new ArgumentException($"Negative exponents not supported: {exponent}");
                if ((exponent % 1) != 0)
                    FractionalExponents = true;

            }
        }
        public override string ToString()
        {
            List<string> terms = new List<string>();
            foreach (double exponent in _exponentToCoefficientMap.Keys)
            {
                terms.Add(FormatTerm(exponent, _exponentToCoefficientMap[exponent]));

            }
            terms.Reverse();
            
            return $"f(x)={string.Join(" + ", terms)}";
        }

        /// <summary>
        /// Evaluate the polynomial at a given input value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double Evaluate(double value)
        {
            if ((value < 0) && FractionalExponents)
                throw new ArgumentException($"Fractional exponents not supported for negative inputs {value}");
            double total = 0;
            foreach (double e in _exponentToCoefficientMap.Keys)
            {
                double coefficient = _exponentToCoefficientMap[e];
                total += (coefficient * (Math.Pow(value, e)));

            }
            return total;
        }

        /// <summary>
        /// Needed in case the user tries to evaluate the function with a negative input.
        /// </summary>
        public bool FractionalExponents { get; private set; }

        #region Implementation

        //String-ormat a single term (e.g. 2x) in the polynomial.
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

        private SortedDictionary<double, double> _exponentToCoefficientMap;
        #endregion
    }
}
