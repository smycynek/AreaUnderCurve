using System;
using Xunit;
using AreaUnderCurve.Core;

namespace AreaUnderCurve.Tests
{
    public class PolynomialTests
    {
        [Fact]
        public void TestPolynomial_OK()
        {
            Polynomial poly1 = new Polynomial(
                new System.Collections.Generic.SortedDictionary<double, double>
                {
                    [2] = 2,
                    [1] = 2,
                    [0] = 2
                });

            Assert.Equal(poly1.Evaluate(2), 14);

        }
        [Fact]
        public void Test_Polynomial_StringRep_OK1()
        {
            Polynomial poly1 = new Polynomial(
                 new System.Collections.Generic.SortedDictionary<double, double>
                 {
                     [4] = 2,
                     [2] = 2,
                     [1] = 2,
                     [0] = -2
                 });

            Assert.Equal(poly1.ToString(), "f(x)=2x^4 + 2x^2 + 2x + -2");
        }

        [Fact]
        public void Test_Polynomial_StringRep_OK2()
        {
            Polynomial poly1 = new Polynomial(
                 new System.Collections.Generic.SortedDictionary<double, double>
                 {
                     [0] = 0,
                 });

            Assert.Equal(poly1.ToString(), "f(x)=0");
        }


        [Fact]
        public void Test_Polynomial_StringRep_OK3()
        {
            Polynomial poly1 = new Polynomial(
                 new System.Collections.Generic.SortedDictionary<double, double>
                 {
                     [0] = 5,
                 });

            Assert.Equal(poly1.ToString(), "f(x)=5");
        }

        [Fact]
        public void TestPolynomialNegativeExponentFail()
        {
            Assert.Throws<ArithmeticException>( () => new Polynomial(
             new System.Collections.Generic.SortedDictionary<double, double>
             {
                 [-4] = 2
             }));

        }

        [Fact]
        public void TestFractionLExponentWithNegativeBoundFail()
        {
            Polynomial poly1 =  new Polynomial(
            new System.Collections.Generic.SortedDictionary<double, double>
            {
                [1.5] = 2
            });
            Assert.Throws<ArithmeticException>(() => poly1.Evaluate(-1));

        }

        [Fact]
        public void TestFractionalExponent_OK()
        {
            Polynomial poly1 = new Polynomial(
          new System.Collections.Generic.SortedDictionary<double, double>
          {
              [1.5] = 1
          });

            Assert.Equal(poly1.Evaluate(2), 2 * Math.Sqrt(2));

        }
    }
}
