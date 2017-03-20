using System;
using System.Collections.Generic;
using System.Text;

namespace AreaUnderCurve.Core
{
   public class AreaUnderCurve
    {
        public static double Calculate(Polynomial polynomial, Bounds bounds, Func<Polynomial, double, double, double> algorithm)
        {
            int boundsIndex = 0;
            double total = 0;
            foreach(double val in bounds.FullRange) // 0-10, count = 11, ubound = 10
            {
                if (boundsIndex == bounds.FullRange.Count - 1)
                    return total;
                double slice = (algorithm(polynomial, val, bounds.FullRange[boundsIndex + 1]));
                // Utility.Log($"{val}, {bounds.FullRange[boundsIndex + 1]}, {slice}");
                total += slice;
                boundsIndex++;
            }
            return 0;
        }
    }
}
