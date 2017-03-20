using AreaUnderCurve.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
namespace AreaUnderCurve.App   

{
    class Program
    {
        static void Main(string[] args)
        {

            if (!RawParameters.TryGetRawParameters(args, out var rawParameters))
            {
                return;
            }
            ParameterManager parameterManager;
            try
            {
                parameterManager = new ParameterManager(rawParameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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