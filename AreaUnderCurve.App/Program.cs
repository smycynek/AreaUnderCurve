using AreaUnderCurve.Core;
using System;
using System.Collections.Generic;
using CommandLineParser;
using CommandLineParser.Arguments; 
using System.Linq.Expressions;
using System.Linq;
namespace AreaUnderCurve.App
{
    class Program
    {
        static void Main(string[] args)
        {

            ParameterManager parameterManager;

            try
            {
                parameterManager = new ParameterManager(args);
            }
            catch (Exception ex)
            {
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