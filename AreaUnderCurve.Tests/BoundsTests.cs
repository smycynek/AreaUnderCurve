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
            AreaUnderCurve.Core.Bounds bounds = new Bounds(2, 4, .1);
            Assert.Equal(bounds.LowerBound, 2);
            Assert.Equal(bounds.UpperBound, 4);
            Assert.Equal(bounds.StepSize, .1);
        }

        [Fact]
        public void Test_Bounds_Bad_StepSize()
        {
            Assert.Throws<ArgumentException>(() => new Bounds(2, 4, 0));

        }

        [Fact]
        public void Test_Bounds_Bad_StepSize2()
        {
            Assert.Throws<ArgumentException>(() => new Bounds(2, 4, -.1));

        }

        [Fact]
        public void Test_Bounds_Bad_Bounds()
        {
            Assert.Throws<ArgumentException>(() => new Bounds(2, 2, 1));

        }

        [Fact]
        public void Test_Bounds_Bad_Bounds2()
        {
            Assert.Throws<ArgumentException>(() => new Bounds(2, 1, 1));

        }


        [Fact]
        public void Test_Bounds_ToString()
        {
            var bounds = new Bounds(0, 5, .1);
            Assert.Equal(bounds.ToString(), "[0 - 5] : StepSize: 0.1");
        }

        [Fact]
        public void Test_Bounds_Range_Length()
        {
            var bounds = new Bounds(0, 5, .1);
            Assert.Equal(bounds.FullRange.Count, 51);
        }

    }
}
