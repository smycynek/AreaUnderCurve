using System;
using System.Collections.Generic;
using System.Text;
using AreaUnderCurve.Core;
using Microsoft.Extensions.Configuration;

namespace AreaUnderCurve.App
{
    public class RawParameters
    {
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public double StepSize { get; set; }
        public string Polynomial { get; set; }
        public string Algorithm { get; set; }

        public static bool TryGetRawParameters(string[] args, out RawParameters parameters)
        {

            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddInMemoryCollection(GetParameterDictionary()).AddCommandLine(args);        
        

            try
            {
                IConfiguration config = configBuilder.Build();
                var polynomial = config.GetValue<string>("polynomial");
                var lowerBound = config.GetValue<double>("lowerBound");
                var upperBound = config.GetValue<double>("upperBound");
                var stepSize = config.GetValue<double>("stepSize");
                var algorithm = config.GetValue<string>("algorithm");

                parameters = new RawParameters { Algorithm = algorithm, LowerBound = lowerBound, UpperBound = upperBound, StepSize = stepSize, Polynomial = polynomial };
                return true;
            }
            catch (Exception ex)
            {
                parameters = null;
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        #region Implementation

        private RawParameters() { }

        private static Dictionary<string, string> GetParameterDictionary()
        {
            return new Dictionary<string, string>
                {
                                    {"polynomial", ""},
                                    {"lowerBound", "0"},
                                    {"upperBound", "10"},
                                    {"stepSize", "1"},
                                    {"algorithm", "Trapezoid"}
                };

        }
        #endregion
    }
}