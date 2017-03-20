using System;
using Xunit;
using AreaUnderCurve.Core;

namespace AreaUnderCurve.Tests
{
    public class PolynomialTests
    {
        [Fact]
        public void Test_Polynomial_Int_OK()
        {
            Polynomial poly1 = new Polynomial(
                new System.Collections.Generic.SortedDictionary<double, double>
                {
                    [2] = 3,
                    [1] = 4,
                    [0] = 5
                });

            Assert.Equal(poly1.Evaluate(-2), 9);
            Assert.Equal(poly1.Evaluate(0), 5);
            Assert.Equal(poly1.Evaluate(2), 25);
        }
        [Fact]
        public void Test_Polynomial_StringRep_OK1()
        {
            Polynomial poly1 = new Polynomial(
                 new System.Collections.Generic.SortedDictionary<double, double>
                 {
                     [0] = -2.5,
                     [1] = 1.5,
                     [3] = 2,
                     [4] = 1
                 });

            Assert.Equal(poly1.ToString(), "f(x)=x^4 + 2x^3 + 1.5x + -2.5");
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
        public void Test_Fractional_Exponent_OK()
        {
            Polynomial poly1 = new Polynomial(
          new System.Collections.Generic.SortedDictionary<double, double>
          {
              [1.5] = 1
          });

            Assert.Equal(poly1.Evaluate(2), 2 * Math.Sqrt(2));

        }
        [Fact]
        public void Test_Fractional_Exponent_With_Negative_Bound_Fail()
        {
            Polynomial poly1 = new Polynomial(
            new System.Collections.Generic.SortedDictionary<double, double>
            {
                [2.5] = 1
            });
            Assert.Throws<ArgumentException>(() => poly1.Evaluate(-2));

        }

        [Fact]
        public void Test_Polynomial_Negative_Exponent_Fail()
        {
            Assert.Throws<ArgumentException>( () => new Polynomial(
             new System.Collections.Generic.SortedDictionary<double, double>
             {
                 [-2] = 1
             }));

        }

        [Fact]
        public void Test_Polynomial_Negative_Fraction_Exponent_Fail()
        {
            Assert.Throws<ArgumentException>(() => new Polynomial(
            new System.Collections.Generic.SortedDictionary<double, double>
            {
                [-2.5] = 1
            }));

        }





    }
}
