using AreaUnderCurve.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
namespace AreaUnderCurve.App   

{
    public class Program
    {

    /*Find approximate area under curve:  Supports simpson, trapezoid, and
    midpoint algorithms, n-degree single variable polynomials, and variable step size
    */
        public static void Main(string[] args)
        {
            //See ParameterManager.Usage for arugments
            if (!RawParameters.TryGetRawParameters(args, out var rawParameters))
            {
                Utility.Log(ParameterManager.Usage);
                return;
            }
            ParameterManager parameterManager;
            try
            {
                parameterManager = new ParameterManager(rawParameters);
            }
            catch (Exception ex)
            {
                Utility.Log(ParameterManager.Usage);
                Utility.Log(ex.Message);
                return;
            }

            Utility.Log(parameterManager.Polynomial.ToString());
            Utility.Log(parameterManager.Bounds.ToString());
            Utility.Log(parameterManager.AlgorithmName);
            double area = AreaUnderCurve.Core.AreaUnderCurve.Calculate(parameterManager.Polynomial, parameterManager.Bounds, parameterManager.Algorithm);
            Utility.Log($"Area={area}");
            
        }
    }
}