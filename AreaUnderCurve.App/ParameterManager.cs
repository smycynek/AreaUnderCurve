using AreaUnderCurve.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AreaUnderCurve.App
{
    public class ParameterManager
    {
        /// <summary>
        /// Parses and validates raw parameters into higher-level parameters 
        /// (Algorithm function, Bounds object, Polynomial object, valid step size)
        /// 
        /// If this constructor doesn't throw an exception, it is safe to assume to all inputs (and the combination
        /// of inputs) are valid and will yield a meaningful result.
        /// </summary>
        /// <param name="rawParameters"></param>
        public ParameterManager(RawParameters rawParameters)
        {
            _rawParameters = rawParameters;
            Init(rawParameters);
          }
        public string AlgorithmName { get; private set; }
        public Func<Polynomial, double, double, double> Algorithm { get; private set; }
        public Bounds Bounds { get; private set; }
        public Polynomial Polynomial { get; private set; }
        #region Implementation

        /// <summary>
        /// Does the high-level validation of all parameters and parses them into high-level objects
        /// </summary>
        /// <param name="rawParameters"></param>
        private void Init(RawParameters rawParameters)
        {
           
            AlgorithmName = _rawParameters.Algorithm;
            Polynomial = GetPolynomial();
            Algorithm = GetAlgorithm();

            Bounds = GetBounds();
            if (Polynomial.FractionalExponents && (Bounds.LowerBound < 0 || Bounds.UpperBound < 0))
                throw new ArgumentException($"Fractional exponents not supported with negative bounds: {Bounds.LowerBound} {Bounds.UpperBound}");
        }

        /// <summary>
        /// Extracts a pair of doubles from a colon-delimted string.
        /// </summary>
        /// <param name="pair"></param>
        /// <returns></returns>
        private Tuple<double, double> ParsePair(string pair)
        {
            if (string.IsNullOrEmpty(pair))
                throw new ArgumentException(pair);
            var values = pair.Split(':');
            if (values.Length != 2)
                throw new ArgumentException($"Invalid pair size: {values.Length}");

            var exponentSuccess = double.TryParse(values[0], out double exponent);
            var coefficientSuccess = double.TryParse(values[1], out double coefficient);
            if (exponentSuccess && coefficientSuccess)
            {
                return new Tuple<double, double>(exponent, coefficient);
            }
            else
                throw new ArgumentException($"Invalid numerical arguments: {values[0]} {values[1]}");

        }

        /// <summary>
        /// Parsees the "{exponent1:coefficient1, exponent2:coefficient1,...}" formatted argument into an array of strings.
        /// </summary>
        /// <param name="dictionaryLiteral"></param>
        /// <returns></returns>
        private string[] ParseDictionaryLiteral(string dictionaryLiteral)
        {
            if (string.IsNullOrEmpty(dictionaryLiteral))
                return null;
            
            var pairs = dictionaryLiteral.TrimEnd('}').TrimStart('{');
            return pairs.Split(',');

        }
        /// <summary>
        /// Parses the string pairs from the command-line into a [double, double] dictionary
        /// representing a polynomial
        /// </summary>
        /// <returns></returns>
        private Polynomial GetPolynomial()
        {
            SortedDictionary<double, double> coefficients = new SortedDictionary<double, double>();
            var pairs = ParseDictionaryLiteral(_rawParameters.Polynomial);
            if (pairs == null)
            {
                throw new ArgumentException($"Invalid polynomial string: {_rawParameters.Polynomial}");

            }
            foreach (string pair in pairs)
            {
                var doublePair = ParsePair(pair);
                coefficients[doublePair.Item1] = doublePair.Item2;
            }
            return new Polynomial(coefficients);

        }

        /// <summary>
        /// Validates properties of Bounds data and returns the bounds object.
        /// </summary>
        /// <returns></returns>
        private Bounds GetBounds()
        {
            if (_rawParameters.LowerBound >= _rawParameters.UpperBound)
            {
                throw new ArgumentException($"LowerBound >= UpperBound: {_rawParameters.LowerBound} {_rawParameters.UpperBound}");
            }
            if (_rawParameters.StepSize <=0)
            {
                throw new ArgumentException($"{nameof(_rawParameters.StepSize)} <=0 : {_rawParameters.StepSize}");
            }
            return new Bounds(_rawParameters.LowerBound, _rawParameters.UpperBound, _rawParameters.StepSize);

        }


        /// <summary>
        /// Get an algorithm by name from the Algorithms class
        /// </summary>
        /// <returns></returns>
        private Func<Polynomial, double, double, double> GetAlgorithm()
        {
            Func<Polynomial, double, double, double> algorithm;
            try
            {
                algorithm = Algorithms.GetAlgorithm(_rawParameters.Algorithm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return algorithm;
        }

        private RawParameters _rawParameters = null;
        #endregion

        public static readonly string Usage = "USAGE: dotnet AreaUnderCurve.App.dll (or AreaUnderCurve.App.exe) /polynomial {DegreeN1:CoefficientM1, DegreeN2:CoefficientM2, ...}... /lowerBound <lower bound> /upperBound <upper bound> /stepSize <step size> /algorithm <Simpson | Trapezoid | Midpoint | RombergNM>" + Environment.NewLine +
            "Example: dotnet AreaUnderCurve.App.exe /polynomial {3:1} /lowerBound 0 /upperBound 10, stepSize 2 /algorithm Romberg32";

    }

}
