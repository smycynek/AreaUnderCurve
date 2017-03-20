using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AreaUnderCurve.Core
{
    public class Bounds
    {

        public Bounds(double lowerBound, double upperBound, double stepSize)
        {
            if (stepSize <= 0)
                throw new ArithmeticException($"{nameof(stepSize)} must be > 0: {stepSize}");

            if (lowerBound >= upperBound)
                throw new ArithmeticException($"Invalid bounds: {lowerBound} {upperBound}");

            LowerBound = lowerBound;
            UpperBound = upperBound;
            StepSize = stepSize;
            FullRange = StepRange(LowerBound, UpperBound, StepSize).ToList<double>();   
        }

        public override string ToString()
        {
            return $"[{LowerBound} - {UpperBound}] : StepSize: {StepSize}";
        }

        private static IEnumerable<double> StepRange(double lowerBound, double upperBound, double stepSize)
        {
            double val;
            for (val = lowerBound; val <= upperBound; val += stepSize)
                yield return val;
        }

        public double LowerBound { get; private set; }
        public double UpperBound { get; private set; }
        public double StepSize { get; private set; }

        public List<double> FullRange { get; private set; }
    }
}
