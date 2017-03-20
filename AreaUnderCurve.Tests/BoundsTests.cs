using System;
using Xunit;
using AreaUnderCurve.Core;

namespace AreaUnderCurve.Tests
{
    public class BoundsTests
    {
        [Fact]
        public void TestBounds_OK()
        {
            AreaUnderCurve.Core.Bounds bounds = new Bounds(0, 10, .1);
            Assert.Equal(bounds.LowerBound, 0);
            Assert.Equal(bounds.UpperBound, 10);
            Assert.Equal(bounds.StepSize, .1);
        }

        [Fact]
        public void TestBounds_BadBounds()
        {
            Assert.Throws<ArithmeticException>(() => new Bounds(0, 0, 1));

        }

        [Fact]
        public void TestBounds_BadStep()
        {
            Assert.Throws<ArithmeticException>(() => new Bounds(0, 1, -1));

        }
        [Fact]
        public void TestBounds_ToString()
        {
            var bounds = new Bounds(0, 5, .1);
            Assert.Equal(bounds.ToString(), "[0 - 5] : StepSize: 0.1");
        }

        [Fact]
        public void TestBounds_RangeLength()
        {
            var bounds = new Bounds(0, 5, .1);
            Assert.Equal(bounds.FullRange.Count, 51);
        }

    }
}
