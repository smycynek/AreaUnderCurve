using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AreaUnderCurve.Core
{

    /// <summary>
    /// Simple class to represent the boundaries under a curve and an approximation step-size.
    /// </summary>
    public class Bounds
    {
        public Bounds(double lowerBound, double upperBound, double stepSize)
        {
            if (stepSize <= 0)
                throw new ArgumentException($"{nameof(stepSize)} must be > 0: {stepSize}");

            if (lowerBound >= upperBound)
                throw new ArgumentException($"Invalid bounds: {lowerBound} {upperBound}");

            LowerBound = lowerBound;
            UpperBound = upperBound;
            StepSize = stepSize;
            FullRange = StepRange(LowerBound, UpperBound, StepSize).ToList<double>();   
        }

        public override string ToString()
        {
            return $"[{LowerBound} - {UpperBound}] : StepSize: {StepSize}";
        }


        public double LowerBound { get; private set; }
        public double UpperBound { get; private set; }
        public double StepSize { get; private set; }

        public List<double> FullRange { get; private set; }

        #region Implementation
        private static IEnumerable<double> StepRange(double lowerBound, double upperBound, double stepSize)
        {
            double val;
            for (val = lowerBound; val <= upperBound; val += stepSize)
                yield return val;
        }
        #endregion

    }
}
