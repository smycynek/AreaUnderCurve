using System;
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

            var bounds = new Bounds(0, 5, 5); //Step size 5 (one segment)
            var polynomial = new Polynomial(new SortedDictionary<double, double> { [4] = 1 }); //# f(x) = x^4, integral from 0-5 = (5^5)/5 = 625 
            var algorithm = Algorithms.GetAlgorithm("Romberg21");
            var area = AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomial, bounds, algorithm);
            Assert.Equal(area, 627, 0);
        }

        [Fact]
        public void Test_Simple_Area_2_Romberg()
        {

            var bounds = new Bounds(0, 5, 5); //Step size 5 (one segment)
            var polynomial = new Polynomial(new SortedDictionary<double, double> { [4] = 1 }); //# f(x) = x^4 , ""
            var algorithm = Algorithms.GetAlgorithm("Romberg32");
            var area = AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomial, bounds, algorithm);
            Assert.Equal(area, 625, 2);
        }

        [Fact]
        public void Test_Simple_Area_3_Romberg()
        {
            var bounds = new Bounds(0, 5, 5); //Step size 5 (one segment)
            var polynomial = new Polynomial(new SortedDictionary<double, double> { [4] = 1 }); //# f(x) = x^4 , ""
            var algorithm = Algorithms.GetAlgorithm("Romberg");
            var area = AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomial, bounds, algorithm);
            Assert.Equal(area, 625, 2);
        }

        [Fact]
        public void Test_Simple_Area_4_Romberg()
        {
            var bounds = new Bounds(0, 5, 5); //Step size 5 (one segment)
            var polynomial = new Polynomial(new SortedDictionary<double, double> { [.5] = 1 }); //integral of f(x)=x^.5 is (x^1.5)1.5 + sc, or (5*sqrt(5))/1.5 with these bounds
            var algorithm = Algorithms.GetAlgorithm("Romberg54");
            var area = AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomial, bounds, algorithm);
            Assert.Equal(area, (5*(Math.Sqrt(5)/1.5)), 2);
        }

        [Fact]
        public void Test_Simple_Area_5_Romberg()
        {
            var bounds = new Bounds(0, 5, 5); //Step size 5 (one segment)
            var polynomial = new Polynomial(new SortedDictionary<double, double> { [.5] = 1 }); //integral of f(x)=x^.5 is (x^1.5)1.5 + sc, or (5*sqrt(5))/1.5 with these bounds
            var algorithm = Algorithms.GetAlgorithm("Romberg43");
            var area = AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomial, bounds, algorithm);
            Assert.Equal(area, (5 * (Math.Sqrt(5) / 1.5)), 0);  //Not quite as accurate with fewer iterations.
        }

        [Fact]
        public void Test_Simple_Area_6_Romberg()
        {
            var bounds = new Bounds(0, 5, 1);  //Use a step size of 1 (5 segments)
            var polynomial = new Polynomial(new SortedDictionary<double, double> { [.5] = 1 }); //integral of f(x)=x^.5 is (x^1.5)1.5 + sc, or (5*sqrt(5))/1.5 with these bounds
            var algorithm = Algorithms.GetAlgorithm("Romberg43");
            var area = AreaUnderCurve.Core.AreaUnderCurve.Calculate(polynomial, bounds, algorithm);
            Assert.Equal(area, (5 * (Math.Sqrt(5) / 1.5)), 2);  //Not quite as accurate with fewer iterations, but we pre-subdivide, which gives more accuracy.
            //We could call this a "pre-quadratured Romberg"
        }


        [Fact]
        public void Test_Simple_Area_3_Romber_InvalidParams()
        {
            Assert.Throws<ArgumentException>(() => Algorithms.GetAlgorithm("Romberg23"));          //N must be greater than M
        }

    }
}
