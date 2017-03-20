using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AreaUnderCurve.Core;
using AreaUnderCurve.App;


namespace AreaUnderCurve.Tests
{
    public class ArgumentParseTests
    {
        [Fact]
        public void TestParameterManager_OK()
        {
            RawParameters.TryGetRawParameters(new string[] { "/polynomial", "{3.5:1,0:2}", "/lowerBound", "2", "/upperBound", "5", "/stepSize", ".2", "/algorithm", "Simpson" }, out var rawParameters);
            ParameterManager pm = new ParameterManager(rawParameters);
            Assert.NotNull(pm.Algorithm);
            Assert.Equal(pm.AlgorithmName, "Simpson");
            Assert.NotNull(pm.Bounds);
            Assert.NotNull(pm.Polynomial);
            Assert.Equal(pm.Bounds.LowerBound, 2);
            Assert.Equal(pm.Bounds.UpperBound, 5);
            Assert.Equal(pm.Bounds.StepSize, .2);
            Assert.True(pm.Polynomial.FractionalExponents);
            Assert.Equal(pm.Polynomial.ToString(), "f(x)=x^3.5 + 2");

        }

        [Fact]
        public void TestParameterManager_OK_Defaults()
        {
            RawParameters.TryGetRawParameters( new string[] { "/polynomial", "{3.5:1,0:2}" }, out var rawParameters);
            ParameterManager pm = new ParameterManager(rawParameters);
            Assert.NotNull(pm.Algorithm);
            Assert.Equal(pm.AlgorithmName, "Trapezoid");
            Assert.NotNull(pm.Bounds);
            Assert.NotNull(pm.Polynomial);
            Assert.Equal(pm.Bounds.LowerBound, 0);
            Assert.Equal(pm.Bounds.UpperBound, 10);
            Assert.Equal(pm.Bounds.StepSize, 1);
            Assert.True(pm.Polynomial.FractionalExponents);
            Assert.Equal(pm.Polynomial.ToString(), "f(x)=x^3.5 + 2");
        }

        [Fact]
        public void TestParameterManager_Reject_Negative_Bounds_With_Fractions()
        {
            RawParameters.TryGetRawParameters(new string[] { "/polynomial", "{3.5:1,0:2}", "/lowerBound", "-5" }, out var rawParameters);
            Assert.Throws<ArgumentException>(()=> new ParameterManager(rawParameters)); 
        }

        [Fact]
        public void TestParameterManager_Negative_double_digit_bounds_OK()
        {
            RawParameters.TryGetRawParameters(new string[] { "/polynomial", "{1:1,0:2}", "/lowerBound", "-10.0" }, out var rawParameters);
            ParameterManager pm = new ParameterManager(rawParameters);
            Assert.Equal(pm.Bounds.LowerBound, -10);


        }
    }
}
