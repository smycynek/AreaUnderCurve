﻿using AreaUnderCurve.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AreaUnderCurve.App
{
    public class ParameterManager
    {
        public ParameterManager(RawParameters rawParameters)
        {
            _rawParameters = rawParameters;
            Init(rawParameters);
  
            if (Polynomial.FractionalExponents && (Bounds.LowerBound < 0 || Bounds.UpperBound < 0))
                throw new ArgumentException($"Fractional exponents not supported for negative bounds. {Bounds.LowerBound} {Bounds.UpperBound}");
        }
        public string AlgorithmName { get; private set; }
        public Func<Polynomial, double, double, double> Algorithm { get; private set; }
        public Bounds Bounds { get; private set; }
        public Polynomial Polynomial { get; private set; }
        #region Implementation
        private void Init(RawParameters rawParameters)
        {
            AlgorithmName = _rawParameters.Algorithm;
            Polynomial = GetPolynomial();
            Algorithm = GetAlgorithm();

            Bounds = GetBounds();
            if (Polynomial.FractionalExponents && (Bounds.LowerBound < 0 || Bounds.UpperBound < 0))
                throw new ArgumentException($"Fractional exponents not supported with negative bounds: {Bounds.LowerBound} {Bounds.UpperBound}");
        }
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
        private string[] ParseDictionaryLiteral(string dictionaryLiteral)
        {
            if (string.IsNullOrEmpty(dictionaryLiteral))
                return null;

            var pairs = dictionaryLiteral.TrimEnd('}').TrimStart('{');
            return pairs.Split(',');

        }
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

    }

}
