using System;
using System.Collections.Generic;
using System.Text;
using AreaUnderCurve.Core;
using CommandLineParser.Arguments;

namespace AreaUnderCurve.App
{

    internal class RawParameters
    {
        [ValueArgument(typeof(double), 'l', "lowerBound", Description = "Set lower bound", DefaultValue = 0, Optional = true)]
        public double LowerBound;

        [ValueArgument(typeof(double), 'u', "upperBound", Description = "Set upper bound", DefaultValue = 10, Optional = true)]
        public double UpperBound;

        [BoundedValueArgument(typeof(double), 's', "stepSize", Description = "Set step size", MinValue = float.Epsilon, MaxValue = float.MaxValue, Optional = true, UseMinValue = true)]
        public double StepSize;

        [EnumeratedValueArgument(typeof(string), 'a',
            LongName = "algorithm", Description = "Set algorithm: Trapezoid|Midpoint|Simpson",
            AllowedValues = "Trapezoid;Midpoint;Simpson", AllowMultiple = false, Optional = true)]
        public string Algorithm;

        [ValueArgument(typeof(string), 'p', "polynomial", Description = "Set polynomial", DefaultValue = "", Optional = false)]
        public string PolynomialStr;


    }
}