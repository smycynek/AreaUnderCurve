using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AreaUnderCurve.Core;
using AreaUnderCurve.App;
using CommandLineParser.Exceptions;

namespace AreaUnderCurve.Tests
{
    public class ArgumentParseTests
    {
        [Fact]
        public void TestParameterManager_OK()
        {
            ParameterManager pm = new ParameterManager(new string[] { "/p", "{3.5:1,0:2}", "/l", "2", "/u", "5", "/s", ".2", "/a", "Simpson"});
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
            ParameterManager pm = new ParameterManager(new string[] { "/p", "{3.5:1,0:2}" });
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
            //must fix -10 issue!
            Assert.Throws<ArgumentException> (() => new ParameterManager(new string[] { "/p", "{3.5:1,0:2}", "/l", "-5" }));
     
        }

        [Fact]
        public void TestParameterManager_Negative_double_digit_bounds_OK()
        {
            //must fix -10 issue!
            var paramMgr =  new ParameterManager(new string[] { "/p", "{1:1,0:2}", "/l", "-10.0" });
            Assert.Equal(paramMgr.Bounds.LowerBound, -10);


        }
    }
}
