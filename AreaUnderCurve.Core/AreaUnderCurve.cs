using System;
using System.Collections.Generic;
using System.Text;

namespace AreaUnderCurve.Core
{
    /// <summary>
    /// High-level implementation class -- calls supplied algorithm with the given polynomial over each consecutive pair of indices in the Bounds parameter.
    /// </summary>
   public class AreaUnderCurve
    {
        public static double Calculate(Polynomial polynomial, Bounds bounds, Func<Polynomial, double, double, double> algorithm)
        {
            int boundsIndex = 0;
            double total = 0;
            foreach(double val in bounds.FullRange) 
            {
                if (boundsIndex == bounds.FullRange.Count - 1) //Stop at range[upper_bound -1], range[upper_bound]
                    return total;
                double slice = (algorithm(polynomial, val, bounds.FullRange[boundsIndex + 1]));
                total += slice;
                boundsIndex++;
            }
            return 0;
        }
    }
}
