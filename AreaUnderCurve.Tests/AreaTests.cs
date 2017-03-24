﻿using System;
using System.Collections.Generic;
using System.Text;
using AreaUnderCurve.Core;
using Xunit;

namespace AreaUnderCurve.Tests
{
    public class AreaTests
    {
        [Fact]
        public void Test_Simple_Area_1()
        {
            var bounds = new Bounds(0, 10, .1);
            var polynomial = new Polynomial(new SortedDictionary<double, double> { [1] = 1 }); // f(x) = x
            var algorithm = Algorithms.GetAlgorithm("Trapezoid");
            var area = AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomial, bounds, algorithm);
            Assert.Equal(area, 50, 2);
        }

        [Fact]
        public void Test_Simple_Area_2()
        {
            var bounds = new Bounds(0, 10, .1);
            var polynomial = new Polynomial(new SortedDictionary<double, double> { [2] = 1 });// # f(x) = x^2
            var algorithm = Algorithms.GetAlgorithm("Simpson");
            var area = AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomial, bounds, algorithm);
            Assert.Equal(area, 1000.0 / 3.0,2);
        }
        [Fact]
        public void Test_Simple_Area_3()
        {

            var bounds = new Bounds(-5, 5, .1);
            var polynomial = new Polynomial(new SortedDictionary<double, double> { [3] = 1 }); //# f(x) = x^3
            var algorithm = Algorithms.GetAlgorithm("Midpoint");
            var area = AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomial, bounds, algorithm);
            Assert.Equal(area, 0, 2);
        }

        [Fact]
        public void Test_Simple_Area_4()
        {

            var bounds = new Bounds(-5, 5, 1);
            var polynomial = new Polynomial(new SortedDictionary<double, double> { [3] = 1 }); //# f(x) = x^3
            var algorithm = Algorithms.GetAlgorithm("Midpoint");
            var area = AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomial, bounds, algorithm);
            Assert.Equal(area, 0, 2);
        }

        [Fact]
        public void Test_Simple_Area_5()
        {

            var bounds = new Bounds(-5, 5, .01);
            var polynomial = new Polynomial(new SortedDictionary<double, double> { [3] = 1 }); //# f(x) = x^3
            var algorithm = Algorithms.GetAlgorithm("Midpoint");
            var area = AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomial, bounds, algorithm);
            Assert.Equal(area, 0, 2);
        }

        [Fact]
        public void Test_Simple_Area_1_Romberg()
        {

            var polynomial = new Polynomial(new SortedDictionary<double, double> { [3] = 1 }); //# f(x) = x^3
            var area = AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomial, bounds, algorithm);
            Assert.Equal(area, 0, 2);
        }

    }
}
